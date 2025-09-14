using FinanceManager.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.FeaturesStoredProcedure.TransactionCategory.Commands.CreateTransactionCategory
{
    public class CreateTransactionCategorySpCommandValidator : AbstractValidator<CreateTransactionCategorySpCommand>
    {

        public CreateTransactionCategorySpCommandValidator(IApplicationDbContext _context )
        {
             RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MustAsync(async (name, cancellation) =>
            {
                var exists = await _context.TransactionCategories.AnyAsync(tc => tc.Name == name);
                return !exists;// true = valid, false = invalid
            })
            .WithMessage("Transaction category with this  name already exists");
            
            RuleFor(c => c.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

             RuleFor(c => c.Type)
            .IsInEnum().WithMessage("Type is invalid.");
        }

    }
}


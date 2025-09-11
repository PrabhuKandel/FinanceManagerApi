using FinanceManager.Application.FeaturesStoredProcedure.TransactionCategory.Commands.CreateTransactionCategory;
using FinanceManager.Infrastructure.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.FeaturesStoredProcedure.TransactionCategory.Commands.UpdateTransactionCategory
{
    public class UpdateTransactionCategorySpCommandValidator : AbstractValidator<UpdateTransactionCategorySpCommand>
    {

        public UpdateTransactionCategorySpCommandValidator(ApplicationDbContext _context)
        {

            RuleFor(x => x.Id)
           .NotEmpty().WithMessage("Id is required.")
           .MustAsync(async (id, cancellation) =>
           {
               return await _context.TransactionCategories.AnyAsync(tc => tc.Id == id, cancellation);
           })
           .WithMessage("Transaction category not found.");

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

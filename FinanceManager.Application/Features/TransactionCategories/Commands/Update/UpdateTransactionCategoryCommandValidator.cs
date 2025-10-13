

using FinanceManager.Application.Features.TransactionCategories.Commands.Update;
using FinanceManager.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.TransactionCategories.Commands.Create
{
    public class UpdateTransactionCategoryCommandValidator:AbstractValidator<UpdateTransactionCategoryCommand>
    {
        public UpdateTransactionCategoryCommandValidator(IApplicationDbContext _context)
        {

            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Id is required");

            RuleFor(c => c.Name)
                   .NotEmpty().WithMessage("Name is required.")
                    .MustAsync(async (command,name, cancellation) =>
                    {
                        var exists = await _context.TransactionCategories.AnyAsync(tc => tc.Name == name && tc.Id != command.Id, cancellation);
                        return !exists;// true = valid, false = invalid
                    })
                   .WithMessage("Transaction category with this  name already exists");

            RuleFor(c => c.Description)
                .MaximumLength(250).WithMessage("Description cannot exceed 250 characters.");

            RuleFor(c => c.Type)
                .IsInEnum().WithMessage("Type is invalid.");

        }
    }
}

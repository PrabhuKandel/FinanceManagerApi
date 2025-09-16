using FinanceManager.Application.Features.TransactionCategories.Commands;
using FinanceManager.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Validators.TransactionCategoryValidator.CommandValidator
{
    public class DeleteTransactionCategoryCommandValidator : AbstractValidator<DeleteTransactionCategoryCommand>
    {
        public DeleteTransactionCategoryCommandValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.")
                .MustAsync(async (id, cancellation) =>
                {
                    return await context.TransactionCategories.AnyAsync(pm => pm.Id == id, cancellation);
                })
                .WithMessage("Transaction category not found.");
        }
    }
}

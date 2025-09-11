using FinanceManager.Application.Features.TransactionCategories.Commands;
using FinanceManager.Infrastructure.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.FeaturesStoredProcedure.TransactionCategory.Commands.DeleteTransactionCategory
{
    public class DeleteTransactionCategorySpCommandValidator : AbstractValidator<DeleteTransactionCategorySpCommand>
    {
        public DeleteTransactionCategorySpCommandValidator(ApplicationDbContext context)
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .MustAsync(async (id, cancellation) =>
                {
                    return await context.TransactionCategories.AnyAsync(tc => tc.Id == id, cancellation);
                })
                .WithMessage("Transaction category not found.");
        }
    }
}

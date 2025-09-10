using FinanceManager.Application.Features.TransactionCategories.Queries;
using FinanceManager.Infrastructure.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Validators.TransactionCategoryValidator
{
    public class GetTransactionCategoryByIdQueryValidator : AbstractValidator<GetTransactionCategoryByIdQuery>
    {
        public GetTransactionCategoryByIdQueryValidator(ApplicationDbContext context)
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

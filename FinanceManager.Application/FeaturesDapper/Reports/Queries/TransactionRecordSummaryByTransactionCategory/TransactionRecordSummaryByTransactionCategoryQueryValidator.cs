using FinanceManager.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.FeaturesDapper.Reports.Queries.TransactionRecordSummaryByTransactionCategory
{
    public class TransactionRecordSummaryByTransactionCategoryQueryValidator:AbstractValidator<TransactionRecordSummaryByTransactionCategoryQuery>
    {
   

        public TransactionRecordSummaryByTransactionCategoryQueryValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.TransactionCategoryId)
                .MustAsync((id, ct) => context.TransactionCategories.AnyAsync(c => c.Id == id, ct))
                .WithMessage("Invalid transaction category")
                .When(x => x.TransactionCategoryId.HasValue);

            RuleFor(x => x.FromDate)
                .Must(date => !date.HasValue || date.Value <= DateTime.UtcNow)
                .WithMessage("FromDate cannot exceed current date.");

            RuleFor(x => x.ToDate)
                .Must(date => !date.HasValue || date.Value <= DateTime.UtcNow)
                .WithMessage("ToDate cannot exceed current date.");

            RuleFor(x => x)
                .Must(x => x.FromDate < x.ToDate)
                .WithMessage("FromDate must be earlier than  ToDate.")
                .When(x => x.FromDate.HasValue && x.ToDate.HasValue);





        }
    }
}

using FinanceManager.Application.Interfaces;
using FluentValidation;

namespace FinanceManager.Application.FeaturesDapper.Reports.TransactionRecordSummaryByCategoryType.Queries
{
    public class TransactionRecordSummaryByCategoryTypeQueryValidator:AbstractValidator<TransactionRecordSummaryByCategoryTypeQuery>
    {
        public TransactionRecordSummaryByCategoryTypeQueryValidator(IApplicationDbContext context)
        {
            

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

            RuleFor(x=>x.GroupBy)
                .IsInEnum()
                .WithMessage("Invalid grouping period.");
        }
    }
}

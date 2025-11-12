using FinanceManager.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.FeaturesDapper.Reports.TransactionRecordSummaryByPaymentMethod.Queries
{
    public class TransactionRecordSummaryByPaymentMethodQueryValidator:AbstractValidator<TransactionRecordSummaryByPaymentMethodQuery>
    {
        public TransactionRecordSummaryByPaymentMethodQueryValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.PaymentMethodId)
                .MustAsync((id, ct) => context.PaymentMethods.AnyAsync(pm => pm.Id == id, ct))
                .WithMessage("Invalid payment  methos")
                .When(x => x.PaymentMethodId.HasValue);

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

using FinanceManager.Application.Features.PaymentMethods.Queries;
using FinanceManager.Infrastructure.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Validators.PaymentMethodValidator
{

    public class GetPaymentMethodByIdQueryValidator : AbstractValidator<GetPaymentMethodByIdQuery>
    {
        public GetPaymentMethodByIdQueryValidator(ApplicationDbContext context)
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .MustAsync(async (id, cancellation) =>
                {
                    return await context.PaymentMethods.AnyAsync(pm => pm.Id == id, cancellation);
                })
                .WithMessage("Payment method not found.");
        }
    }
}

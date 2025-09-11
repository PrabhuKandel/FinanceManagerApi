using FinanceManager.Infrastructure.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.FeaturesStoredProcedure.PaymentMethods.Commands.DeletePaymentMethod
{
    public class DeletePaymentMethodSpCommandValidator : AbstractValidator<DeletePaymentMethodSpCommand>
    {
        public DeletePaymentMethodSpCommandValidator(ApplicationDbContext context)
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

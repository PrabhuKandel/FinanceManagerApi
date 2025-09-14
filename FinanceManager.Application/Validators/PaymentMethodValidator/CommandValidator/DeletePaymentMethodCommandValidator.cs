using FinanceManager.Application.Features.PaymentMethods.Commands;
using FinanceManager.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Validators.PaymentMethodValidator.CommandValidator
{
    public class DeletePaymentMethodCommandValidator : AbstractValidator<DeletePaymentMethodCommand>
    {
        public DeletePaymentMethodCommandValidator(IApplicationDbContext context)
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

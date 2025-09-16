using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Features.PaymentMethods.Commands;
using FluentValidation;

namespace FinanceManager.Application.Validators.PaymentMethodValidator.CommandValidator
{
    public class UpdatePaymentMethodCommandValidator : AbstractValidator<UpdatePaymentMethodCommand>
    {

        public UpdatePaymentMethodCommandValidator(IValidator<PaymentMethodUpdateDto> dtoValidator) 
        {

            RuleFor(x => x.PaymentMethod)
                .NotNull().WithMessage("Payment method data is required")
                .SetValidator(dtoValidator);
        }
    }
}

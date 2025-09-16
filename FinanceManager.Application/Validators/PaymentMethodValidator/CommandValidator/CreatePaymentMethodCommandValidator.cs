using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Features.PaymentMethods.Commands;
using FluentValidation;

namespace FinanceManager.Application.Validators.PaymentMethodValidator.CommandValidator
{
    public class CreatePaymentMethodCommandValidator : AbstractValidator<CreatePaymentMethodCommand>
    {

        public CreatePaymentMethodCommandValidator(IValidator<PaymentMethodCreateDto> dtoValidator) 
        {

            RuleFor(x => x.PaymentMethod)
                .NotNull().WithMessage("Payment method data is required")
                .SetValidator(dtoValidator);
        }
    }
}

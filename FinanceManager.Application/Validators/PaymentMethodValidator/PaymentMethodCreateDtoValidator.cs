using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Dtos.TransactionCategory;
using FluentValidation;

namespace FinanceManager.Application.Validators.PaymentMethodValidator
{
    public class PaymentMethodCreateDtoValidator : AbstractValidator<PaymentMethodCreateDto>
    {
        
    
        public PaymentMethodCreateDtoValidator()
        {


            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(c => c.Description)
                .MaximumLength(250).WithMessage("Description cannot exceed 250 characters.");

        }
}
}

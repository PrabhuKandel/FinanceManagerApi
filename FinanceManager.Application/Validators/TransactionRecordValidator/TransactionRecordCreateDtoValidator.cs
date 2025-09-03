using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Dtos.TransactionRecord;
using FluentValidation;

namespace FinanceManager.Application.Validators.TransactionRecordValidator
{
    public class TransactionRecordCreateDtoValidator : AbstractValidator<TransactionRecordCreateDto>
    {
        public TransactionRecordCreateDtoValidator()
        {
            RuleFor(x => x.TransactionCategoryId)
                .NotEmpty().WithMessage("Transaction category is required.");

            RuleFor(x => x.Amount)
                .GreaterThan(0m).WithMessage("Amount must be greater than 0");

            RuleFor(x => x.PaymentMethodId)
                .NotEmpty().WithMessage("Payment method is required.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.TransactionDate)
                .NotEmpty().WithMessage("Transaction date is required.");
        }
    }
}

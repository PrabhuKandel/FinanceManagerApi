using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Features.TransactionCategories.Commands;
using FluentValidation;

namespace FinanceManager.Application.Validators.TransactionCategoryValidator.CommandValidator
{
    public class CreateTransactionCategoryCommandValidator: AbstractValidator<CreateTransactionCategoryCommand>
    {

        public CreateTransactionCategoryCommandValidator(IValidator<TransactionCategoryCreateDto> dtoValidator)
            {
            RuleFor(x => x.TransactionCategory)
            .NotNull().WithMessage("Transaction category data is required")
            .SetValidator(dtoValidator);

        }
    }
}

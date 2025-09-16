using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Features.TransactionCategories.Commands;
using FluentValidation;

namespace FinanceManager.Application.Validators.TransactionCategoryValidator.CommandValidator
{
    public class UpdateTransactionCategoryCommandValidator : AbstractValidator<UpdateTransactionCategoryCommand>
    {

        public UpdateTransactionCategoryCommandValidator(IValidator<TransactionCategoryUpdateDto> dtoValidator)
        {
            RuleFor(x => x.TransactionCategory)
            .NotNull().WithMessage("Transaction category data is required")
            .SetValidator(dtoValidator);

        }
    }
}

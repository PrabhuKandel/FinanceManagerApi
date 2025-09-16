using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Application.Features.TransactionRecords.Commands;
using FluentValidation;

namespace FinanceManager.Application.Validators.TransactionCategoryValidator.CommandValidator
{
    public class CreateTransactionRecordCommandValidator : AbstractValidator<CreateTransactionRecordCommand>
    {

        public CreateTransactionRecordCommandValidator(IValidator<TransactionRecordCreateDto> dtoValidator)
        {
            RuleFor(x => x.TransactionRecord)
            .NotNull().WithMessage("Transaction record data is required")
            .SetValidator(dtoValidator);

        }
    }
}

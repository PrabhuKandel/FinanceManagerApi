using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Validators.TransactionPaymentValidator;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.FeaturesDapper.TransactionRecords.Commands.UpdateTransactionRecord
{
    public class UpdateTransactionRecordDapperCommandValidator : AbstractValidator<UpdateTransactionRecordDapperCommand>
    {
        public UpdateTransactionRecordDapperCommandValidator(IApplicationDbContext _context)
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.TransactionCategoryId)
               .NotEmpty().WithMessage("Transaction category is required.")
               .MustAsync(async (transactionCategoryId, cancellation) =>
               {
                   var exists = await _context.TransactionCategories.AnyAsync(c => c.Id == transactionCategoryId);
                   return exists;// true = valid, false = invalid //valid only if category exists
               })
               .WithMessage("Invalid transaction category");


            RuleFor(x => x.Amount)
                .GreaterThan(0m).WithMessage("Amount must be greater than 0");

            RuleForEach(x => x.Payments)
                .NotEmpty().WithMessage("At least one payment is required.")
               .SetValidator(new TransactionPaymentDtoValidator(_context));

            RuleFor(x => x)
                 .Must(x => x.Amount == x.Payments.Sum(p => p.Amount))
                 .WithMessage("Total transaction amount must equal sum of payments");


            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");


            RuleFor(x => x.TransactionDate)
                .NotEmpty().WithMessage("Transaction date is required.");

        }
    }
}

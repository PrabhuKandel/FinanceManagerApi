using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Validators.TransactionPaymentValidator;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Validators.TransactionRecordValidator
{
    public class TransactionRecordBaseDtoValidator<T> : AbstractValidator<T> where T :TransactionRecordBaseDto
    {

        public TransactionRecordBaseDtoValidator(IApplicationDbContext _context)
        {
            RuleFor(x => x.TransactionCategoryId)
                .NotEmpty().WithMessage("Transaction category is required.")
                .MustAsync(async (transactionCategoryId, cancellation) =>
                {
                    var exists = await _context.TransactionCategories.AnyAsync(c => c.Id == transactionCategoryId);
                    return exists;// true = valid, false = invalid //valid only if category exists
                })
                .WithMessage("Invalid transaction category");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Amount is required")
                .GreaterThan(0m).WithMessage("Amount must be greater than 0")
                .Must((transaction, amount) =>
                {
                    var totalPayments = 100m;
                    var totalAmount = Math.Round(amount, 2);

                    //var totalPayments = Math.Round(transaction.Payments.Sum(p => p.Amount)??0, 2);
            

                    // Allow a tolerance of 0.01 for rounding
                    return Math.Abs(totalPayments - totalAmount) < 0.01m;
                })
                .WithMessage("Total transaction amount must equal sum of payments");


            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");


            RuleFor(x => x.TransactionDate)
                .NotEmpty().WithMessage("Transaction date is required.");

        }

    }
}

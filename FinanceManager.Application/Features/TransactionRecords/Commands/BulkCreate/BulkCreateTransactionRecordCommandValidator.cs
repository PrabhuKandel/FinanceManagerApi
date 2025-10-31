using FinanceManager.Application.Features.TransactionRecords.Commands.BulkCreate;
using FinanceManager.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.TransactionRecords.Commands.Create
{
    public class BulkCreateTransactionRecordCommandValidator : AbstractValidator<BulkCreateTransactionRecordCommand>
    {
        public BulkCreateTransactionRecordCommandValidator(IApplicationDbContext _context)
        {
           
            RuleFor(x => x.TransactionRecords)
                .NotEmpty().WithMessage("At least one transaction is required.");

            // Validate each transaction in the bulk
            RuleForEach(x => x.TransactionRecords).ChildRules(transaction =>
            {
                transaction.RuleFor(tr => tr.TransactionCategoryId)
                    .NotEmpty().WithMessage("Transaction category is required.")
                    .MustAsync(async (transactionCategoryId, cancellation) =>
                    {
                        return await _context.TransactionCategories.AnyAsync(c => c.Id == transactionCategoryId, cancellation);
                    }).WithMessage("Invalid transaction category.");

                transaction.RuleFor(tr => tr.Amount)
                    .GreaterThan(0m).WithMessage("Transaction amount must be greater than 0")
                    .Must((tr, amount) => amount == tr.Payments.Sum(p => p.Amount))
                    .WithMessage("Total transaction amount must equal sum of payments");

                transaction.RuleFor(tr => tr.Description)
                    .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

                transaction.RuleFor(tr => tr.TransactionDate)
                    .NotEmpty().WithMessage("Transaction date is required.");

                // Payments validation
                transaction.RuleForEach(tr => tr.Payments).ChildRules(payment =>
                {
                    payment.RuleFor(p => p.Amount)
                        .GreaterThan(0).WithMessage("Payment amount must be greater than 0");

                    payment.RuleFor(p => p.PaymentMethodId)
                        .NotEmpty().WithMessage("Payment method is required")
                        .MustAsync(async (id, cancellation) =>
                        {
                            return await _context.PaymentMethods
                                .AnyAsync(pm => pm.Id == id && pm.IsActive, cancellation);
                        })
                        .WithMessage("Invalid or inactive payment method");
                });
            });
        }
    }
}

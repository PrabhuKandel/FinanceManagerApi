

using FinanceManager.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.TransactionRecords.Commands.Create
{
    public class CreateTransactionRecordCommandValidator:AbstractValidator<CreateTransactionRecordCommand>
    {
        public CreateTransactionRecordCommandValidator(IApplicationDbContext _context)
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
                .Must((transaction, amount) => amount == transaction.Payments.Sum(p => p.Amount))
                 .WithMessage("Total transaction amount must equal sum of payments");


            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");


            RuleFor(x => x.TransactionDate)
                .NotEmpty().WithMessage("Transaction date is required.");

            // Payments validation
            RuleForEach(x => x.Payments)
                .NotEmpty().WithMessage("At least one payment is required.")
                .ChildRules(payment =>
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
                
        }
    }
}

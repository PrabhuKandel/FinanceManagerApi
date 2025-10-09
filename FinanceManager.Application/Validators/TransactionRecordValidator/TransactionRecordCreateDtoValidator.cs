using FinanceManager.Application.Dtos.TransactionPayment;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Validators.TransactionPaymentValidator;
using FluentValidation;
using Microsoft.EntityFrameworkCore;


namespace FinanceManager.Application.Validators.TransactionRecordValidator
{
    public class TransactionRecordCreateDtoValidator : TransactionRecordBaseDtoValidator<TransactionRecordCreateDto>
    {
        public TransactionRecordCreateDtoValidator(IApplicationDbContext _context) : base(_context)
        {

            // Payments validation
            RuleForEach(x => x.Payments)
                .NotEmpty().WithMessage("At least one payment is required.")
                .ChildRules(payment =>
                {
                    // Amount validation
                    payment.RuleFor(p => p.Amount)
                        .GreaterThan(0).WithMessage("Payment amount must be greater than 0");

                    // PaymentMethodId validation (async)
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

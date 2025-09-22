using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Dtos.TransactionPayment;
using FinanceManager.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Validators.TransactionPaymentValidator
{
    public class TransactionPaymentDtoValidator : AbstractValidator<TransactionPaymentDto>
    {
        public TransactionPaymentDtoValidator(IApplicationDbContext _context)
        { 
             RuleFor(p => p.PaymentMethodId)
                .NotEmpty().WithMessage("Payment method is required.")
                .MustAsync(async (id, cancellation) =>
                {
                    return await _context.PaymentMethods.AnyAsync(pm => pm.Id == id);
                }).WithMessage("Invalid payment method");

            RuleFor(p => p.Amount)
                .GreaterThan(0m).WithMessage("Payment amount must be greater than 0");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Features.PaymentMethods.Commands;
using FinanceManager.Infrastructure.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Validators.PaymentMethodValidator.CommandValidator
{
    public class DeletePaymentMethodCommandValidator : AbstractValidator<DeletePaymentMethodCommand>
    {
        public DeletePaymentMethodCommandValidator(ApplicationDbContext context)
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .MustAsync(async (id, cancellation) =>
                {
                    return await context.PaymentMethods.AnyAsync(pm => pm.Id == id, cancellation);
                })
                .WithMessage("Payment method not found.");
        }
    }

}

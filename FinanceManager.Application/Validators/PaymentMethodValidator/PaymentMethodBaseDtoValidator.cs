using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Infrastructure.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Validators.PaymentMethodValidator
{
    public class PaymentMethodBaseDtoValidator<T> : AbstractValidator<T> where T : PaymentMethodBaseDto
    {
        public PaymentMethodBaseDtoValidator(ApplicationDbContext _context)
        {
            RuleFor(c => c.Name)
             .NotEmpty().WithMessage("Name is required.")
             .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.")
             .MustAsync(async (name, cancellation) =>
             {
                 var exists = await _context.PaymentMethods.AnyAsync(pm => pm.Name == name);
                 return !exists;// true = valid, false = invalid
             })
           .WithMessage("Product name already exists");

            RuleFor(c => c.Description)
                .MaximumLength(200).WithMessage("Description cannot exceed 200 characters.");
        }
    }
}



using FinanceManager.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.PaymentMethods.Commands.Create
{
    public class CreatePaymentMethodCommandValidator: AbstractValidator<CreatePaymentMethodCommand>
    {
        public CreatePaymentMethodCommandValidator(IApplicationDbContext _context)
        {
            RuleFor(c => c.Name)
             .NotEmpty().WithMessage("Name is required.")
             .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.")
             .MustAsync(async (name, cancellation) =>
             {
                 var exists = await _context.PaymentMethods.AnyAsync(pm => pm.Name == name);
                 return !exists;// true = valid, false = invalid
             })
           .WithMessage("Name already exists");

            RuleFor(c => c.Description)
                .MaximumLength(200).WithMessage("Description cannot exceed 200 characters.");

            RuleFor(c => c.IsActive)
                .NotNull().WithMessage("IsActive is required.");
        }
    }
}

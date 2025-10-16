

using FinanceManager.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.PaymentMethods.Commands.Update
{
    public class UpdatePaymentMethodCommandValidator:AbstractValidator<UpdatePaymentMethodCommand>
    {
        public UpdatePaymentMethodCommandValidator(IApplicationDbContext _context)
        {
            RuleFor(c=>c.Id)
                .NotEmpty()
                .WithMessage("Id is required");

            RuleFor(c => c.Name)
             .NotEmpty().WithMessage("Name is required.")
             .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.")
             .MustAsync(async (command,name, cancellation) =>
             {
                 var exists = await _context.PaymentMethods.AnyAsync(pm => pm.Name == name && pm.Id != command.Id);
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

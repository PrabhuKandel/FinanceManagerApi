

using FluentValidation;

namespace FinanceManager.Application.Features.Auth.Commands.ApplicationUserRegister
{
    public class ApplicationUserRegisterCommandValidator:AbstractValidator<ApplicationUserRegisterCommand>
    {
        public ApplicationUserRegisterCommandValidator()
        {
            RuleFor(x => x.FirstName)
         .NotEmpty().WithMessage("First name is required.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(100).WithMessage("Address cannot exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage("Invalid email format.");
        }
    }
}

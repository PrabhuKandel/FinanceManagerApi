using FluentValidation;

namespace FinanceManager.Application.Features.Auth.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator:AbstractValidator<ChangePasswordCommand>
    {

        public ChangePasswordCommandValidator()
        { 
                RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("Current pasword required");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Password is required.")
                .Matches("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[^a-zA-Z0-9]).{8,}$")
                .WithMessage("Password must be at least 8 characters long, " +
                             "contain uppercase and lowercase letters, " +
                             "a number, and a special character.");
        }
    }
}

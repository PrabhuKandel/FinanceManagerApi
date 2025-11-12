

using FluentValidation;

namespace FinanceManager.Application.Features.Auth.Commands.ApplicationUserLogin
{
    public class ApplicationUserLoginCommandValidator:AbstractValidator<ApplicationUserLoginCommand>
    {
        public ApplicationUserLoginCommandValidator()
        {
            RuleFor(x => x.Email)
          .NotEmpty().WithMessage("Email is required.");


            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}

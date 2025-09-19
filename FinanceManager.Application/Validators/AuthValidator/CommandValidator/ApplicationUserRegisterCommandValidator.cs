using FinanceManager.Application.Dtos.ApplicationUser;
using FinanceManager.Application.Features.Auth.Commands;
using FluentValidation;

namespace FinanceManager.Application.Validators.AuthValidator.CommandValidator
{
    public class ApplicationUserRegisterCommandValidator:AbstractValidator<ApplicationUserRegisterCommand>
    {

        public ApplicationUserRegisterCommandValidator(IValidator<ApplicationUserRegisterDto> dtoValidator)
        {
            RuleFor(x => x.RegisterUser)
            .NotNull().WithMessage("Register user data is required")
            .SetValidator(dtoValidator);
        }
    }
}

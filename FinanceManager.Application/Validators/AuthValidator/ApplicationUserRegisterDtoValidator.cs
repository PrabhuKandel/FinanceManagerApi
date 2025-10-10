using FinanceManager.Application.Dtos.ApplicationUser;
using FluentValidation;

namespace FinanceManager.Application.Validators.AuthValidator
{
    public class ApplicationUserRegisterDtoValidator:AbstractValidator<ApplicationUserRegisterDto>
    {

        public ApplicationUserRegisterDtoValidator()
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

            //RuleFor(x => x.Password)
            //    .NotEmpty().WithMessage("Password is required.")
            //    .Matches("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[^a-zA-Z0-9]).{8,}$")
            //    .WithMessage("Password must be at least 8 characters long, " +
            //                 "contain uppercase and lowercase letters, " +
            //                 "a number, and a special character.");



        }

    }
}

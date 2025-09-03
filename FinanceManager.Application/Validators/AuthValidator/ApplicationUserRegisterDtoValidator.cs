using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

                RuleFor(x => x.Password)
          .NotEmpty().WithMessage("Password is required.")
          .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
          .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
          .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
          .Matches("[0-9]").WithMessage("Password must contain at least one number.")
          .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");


        }

    }
}

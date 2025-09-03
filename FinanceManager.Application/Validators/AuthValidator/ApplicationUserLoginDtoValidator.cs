using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Dtos.ApplicationUser;
using FluentValidation;

namespace FinanceManager.Application.Validators.AuthValidator
{
    public class ApplicationUserLoginDtoValidator : AbstractValidator<ApplicationUserLoginDto>
    {

        public ApplicationUserLoginDtoValidator() 
        {

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.");
                

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");

        }
    }
}

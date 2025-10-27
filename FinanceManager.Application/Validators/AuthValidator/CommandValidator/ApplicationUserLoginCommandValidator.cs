using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Dtos.ApplicationUser;
using FinanceManager.Application.Features.Auth.Commands;
using FinanceManager.Application.Features.TransactionRecords.Commands;
using FluentValidation;

namespace FinanceManager.Application.Validators.AuthValidator.CommandValidator
{
    public class ApplicationUserLoginCommandValidator : AbstractValidator<ApplicationUserLoginCommand>
    {

        public ApplicationUserLoginCommandValidator(IValidator<ApplicationUserLoginDto> dtoValidator)
        {
            RuleFor(x => x.LoginUser)
            .NotNull().WithMessage("Login user data is required")
            .SetValidator(dtoValidator);

        }
    }
}

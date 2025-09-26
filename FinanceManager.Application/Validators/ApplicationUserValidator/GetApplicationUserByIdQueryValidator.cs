using FinanceManager.Application.Features.ApplicationUsers.Queries.GetApplicationUserById;
using FinanceManager.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Validators.ApplicationUserValidator
{
    public class GetApplicationUserByIdQueryValidator:AbstractValidator<GetApplicationUserByIdQuery>
    {
        public GetApplicationUserByIdQueryValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .MustAsync(async (id, cancellation) =>
                {
                    return await context.ApplicationUsers.AnyAsync(pm => pm.Id == id, cancellation);
                })
                .WithMessage("User doesn't exist ");
        }
    }
}

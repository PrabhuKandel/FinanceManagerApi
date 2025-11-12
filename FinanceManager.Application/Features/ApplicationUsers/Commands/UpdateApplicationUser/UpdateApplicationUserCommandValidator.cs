using FinanceManager.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.ApplicationUsers.Commands.UpdateApplicationUser
{
    public class UpdateApplicationUserCommandValidator:AbstractValidator<UpdateApplicationUserCommand>
    {
        public UpdateApplicationUserCommandValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Id)
     .NotEmpty().WithMessage("User Id is required.")
     .MustAsync(async (id, cancellationToken) =>
     {
         return await context.ApplicationUsers.AnyAsync(u => u.Id == id, cancellationToken);
     }).WithMessage("User with specified Id does not exist.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName is required");

            RuleFor(x => x.LastName)
             .NotEmpty().WithMessage("LastName is required");

            RuleFor(x => x.Address)
             .NotEmpty().WithMessage("Address is required");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not valid.")
                .MustAsync(async (command, email, cancellationToken) =>
                {
                    return !await context.ApplicationUsers
                        .AnyAsync(u => u.Email == email && u.Id != command.Id, cancellationToken);
                }).WithMessage("Email is already taken by another user.");

            RuleFor(x => x.RoleId)
              .NotEmpty().WithMessage("RoleId is required.")
              .MustAsync(async (roleId, cancellationToken) =>
              {
                  return await context.Roles.AnyAsync(r => r.Id == roleId, cancellationToken);
              }).WithMessage("Specified Role does not exist.");
        }
    }
}

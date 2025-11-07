using FinanceManager.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.ApplicationUsers.Commands.AssignRole
{
    public class AssignRoleCommandValidator : AbstractValidator<AssignRoleCommand>
    {
        public AssignRoleCommandValidator(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            RuleFor(x => x.ApplicationUserId)
               .NotEmpty().WithMessage("User ID is required.");


            RuleFor(x => x.RoleNames)
                .NotNull().WithMessage("At least one role must be provided.")
                .Must(list => list.Any()).WithMessage("Role list cannot be empty.")
                .MustAsync(async (roleNames, cancellation) =>
                {
                    var existingRoles = await roleManager.Roles.Select(r => r.Name).ToListAsync();
                    return roleNames.All(name => existingRoles.Contains(name));
                })
                .WithMessage("One or more roles do not exist.");
        }
    }
}

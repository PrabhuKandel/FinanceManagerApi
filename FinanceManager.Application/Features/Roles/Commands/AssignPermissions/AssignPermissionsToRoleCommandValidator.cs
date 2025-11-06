

using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.Roles.Commands.AssignPermissions
{
    public class AssignPermissionsToRoleCommandValidator : AbstractValidator<AssignPermissionsToRoleCommand>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AssignPermissionsToRoleCommandValidator(
            RoleManager<IdentityRole> roleManager,
            IApplicationDbContext _dbContext,
            IPermissionService _permissionService)
        {

            _roleManager = roleManager;

            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("Role ID is required.")
                .MustAsync(RoleExists).WithMessage("Specified role does not exist.");

            RuleFor(x => x.Permissions)
                .NotEmpty().WithMessage("At least one permission must be selected.")
                .Must(permissions=>
                {
                    var validPermissions = _permissionService.GetAllPermissions().ToHashSet();
                    return permissions.All(p => validPermissions.Contains(p));
                })
                    .WithMessage("One or more provided permissions do not exist .");
        }

        
        private async Task<bool> RoleExists(string roleId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(roleId))
                return false;

            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == roleId, cancellationToken);
            return role != null;
        }

     

    }
}

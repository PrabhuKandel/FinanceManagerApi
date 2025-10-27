

using FinanceManager.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.Roles.Commands.AssignPermissions
{
    public class AssignPermissionsToRoleCommandValidator : AbstractValidator<AssignPermissionsToRoleCommand>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IApplicationDbContext _dbContext;

        public AssignPermissionsToRoleCommandValidator(
            RoleManager<IdentityRole> roleManager,
            IApplicationDbContext dbContext)
        {
            _roleManager = roleManager;
            _dbContext = dbContext;

            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("Role ID is required.")
                .MustAsync(RoleExists).WithMessage("Specified role does not exist.");

            RuleFor(x => x.PermissionIds)
                .NotEmpty().WithMessage("At least one permission must be selected.")
                .MustAsync(AllPermissionsExist)
                    .WithMessage("One or more provided permissions do not exist or are inactive.");
        }

        
        private async Task<bool> RoleExists(string roleId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(roleId))
                return false;

            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == roleId, cancellationToken);
            return role != null;
        }

     
        private async Task<bool> AllPermissionsExist(List<Guid> permissionIds, CancellationToken cancellationToken)
        {
            if (permissionIds == null || !permissionIds.Any())
                return false;

            var validCount = await _dbContext.Permissions
                .CountAsync(p => permissionIds.Contains(p.Id) && p.IsActive, cancellationToken);

            return validCount == permissionIds.Count;
        }
    }
}

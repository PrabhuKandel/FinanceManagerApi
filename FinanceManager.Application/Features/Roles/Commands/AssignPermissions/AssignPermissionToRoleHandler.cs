

using System.Security.Claims;
using FinanceManager.Application.Common;
using FinanceManager.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.Roles.Commands.AssignPermissions
{
    public class AssignPermissionToRoleHandler(
        RoleManager<IdentityRole> _roleManager,
        IApplicationDbContext _context
        ) : IRequestHandler<AssignPermissionsToRoleCommand, OperationResult<string>>
    {
        public async Task<OperationResult<string>> Handle(AssignPermissionsToRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == request.RoleId, cancellationToken);

            var permissions = await _context.Permissions
             .Where(p => request.PermissionIds.Contains(p.Id) && p.IsActive)
             .ToListAsync(cancellationToken);

            var existingClaims = await _roleManager.GetClaimsAsync(role!);

            foreach (var permission in permissions)
            {
                if (!existingClaims.Any(c => c.Type == permission.Name))
                    await _roleManager.AddClaimAsync(role!, new Claim(permission.Name, "true"));
            }

            return new OperationResult<string>
            {
                Message= "Permissions assigned successfully",
            };
        }
    }
}

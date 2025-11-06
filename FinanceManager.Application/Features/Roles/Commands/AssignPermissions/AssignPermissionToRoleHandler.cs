using System.Security.Claims;
using FinanceManager.Application.Common;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.Roles.Commands.AssignPermissions
{
    public class AssignPermissionToRoleHandler(
        RoleManager<IdentityRole> _roleManager,
        ICacheService _cacheService
        ) : IRequestHandler<AssignPermissionsToRoleCommand, OperationResult<string>>
    {
        public async Task<OperationResult<string>> Handle(AssignPermissionsToRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == request.RoleId, cancellationToken);


            var existingClaims = await _roleManager.GetClaimsAsync(role!);
            var permissionClaims = existingClaims.Where(c => c.Type == "Permission").ToList();
        
            foreach (var claim in permissionClaims)
            {
                await _roleManager.RemoveClaimAsync(role!, claim);
            }

            // Add all new permissions
            foreach (var permission in request.Permissions.Distinct())
            {
                await _roleManager.AddClaimAsync(role!, new Claim("Permission", permission));
            }

            // Update Redis cache with latest permissions
            await _cacheService.SetAsync(role.Name, request.Permissions.Distinct());

            return new OperationResult<string>
            {
                Message= "Permissions assigned successfully",
            };
        }
    }
}

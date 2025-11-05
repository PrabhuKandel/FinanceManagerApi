using FinanceManager.Infrastructure.Authorization.ClaimTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FinanceManager.Infrastructure.Authorization.Requirements
{


    public class PermissionHandler (RoleManager<IdentityRole> _roleManager): AuthorizationHandler<PermissionRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {

            // 1. Get role claims from JWT
            var roleClaims = context.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role).Select(c => c.Value).ToList();
            if (!roleClaims.Any()) return;


            // 2. Check each role for the required permission
            foreach (var roleName in roleClaims)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role == null) continue;

                var claims = await _roleManager.GetClaimsAsync(role);
                if (claims.Any(c => c.Type == CustomClaimTypes.Permission && c.Value == requirement.PermissionName))
                {
                    context.Succeed(requirement);
                    return;
                }
            }
        }
    }
}

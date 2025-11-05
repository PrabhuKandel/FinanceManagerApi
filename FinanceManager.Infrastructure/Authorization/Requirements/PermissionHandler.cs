

using FinanceManager.Infrastructure.Authorization.ClaimTypes;
using Microsoft.AspNetCore.Authorization;

namespace FinanceManager.Infrastructure.Authorization.Requirements
{


    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {

            bool found = context.User.Claims.Any(c => c.Type == CustomClaimTypes.Permission && c.Value == requirement.PermissionName);
            if (found)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}

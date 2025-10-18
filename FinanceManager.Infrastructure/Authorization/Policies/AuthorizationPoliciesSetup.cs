using FinanceManager.Infrastructure.Authorization.ClaimTypes;
using FinanceManager.Infrastructure.Authorization.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Infrastructure.Authorization.Policies
{
    public static class AuthorizationPoliciesSetup
    {
        public static void AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                foreach (var permission in PermissionHelper.GetAllPermissions())
                {
                    options.AddPolicy(permission, policy =>
                        policy.RequireClaim(CustomClaimTypes.Permission, permission));
                }
            });
        }
    }
}

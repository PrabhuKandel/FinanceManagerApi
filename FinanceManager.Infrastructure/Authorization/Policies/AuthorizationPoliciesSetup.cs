using FinanceManager.Infrastructure.Authorization.ClaimTypes;
using FinanceManager.Infrastructure.Authorization.Permissions;
using FinanceManager.Infrastructure.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Infrastructure.Authorization.Policies
{
    public static class AuthorizationPoliciesSetup
    {
        public static void AddAuthorizationPolicies(this IServiceCollection services)
        {
            //services.AddAuthorization(options =>
            //{
            //    foreach (var permission in PermissionHelper.GetAllPermissions().Select(p => p.Permission))
            //    {
            //        options.AddPolicy(permission, policy =>
            //            policy.RequireClaim(CustomClaimTypes.Permission, permission));
            //    }
            //});

            services.AddAuthorization(options =>
            {
                foreach (var permission in PermissionHelper.GetAllPermissions().Select(p => p.Permission))
                {
                    options.AddPolicy(permission, policy =>
                        policy.Requirements.Add(new PermissionRequirement(permission)));
                }
            });
        }
    }
}

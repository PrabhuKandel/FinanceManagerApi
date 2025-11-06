using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Infrastructure.Authorization.ClaimTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace FinanceManager.Infrastructure.Authorization.Requirements
{


    public class PermissionHandler (
        RoleManager<IdentityRole> _roleManager , 
        ICacheService _cacheService
        ) : AuthorizationHandler<PermissionRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {

            // 1. Get role claims from JWT
            var roleClaims = context.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role).Select(c => c.Value).ToList();
            if (!roleClaims.Any()) return;
            

            // 2. Check each role for the required permission
            foreach (var roleName in roleClaims)

            {
  
                //fetch from cache
                var cachedPermissions = await _cacheService.GetAsync<List<string>>(roleName);

                if (cachedPermissions == null)
                {
                    //not in cache, get from database
                    var role = await _roleManager.FindByNameAsync(roleName);
                    if (role == null) continue;

                    var claims = await _roleManager.GetClaimsAsync(role);
                    var permissions = claims.Where(c => c.Type == CustomClaimTypes.Permission).Select(c => c.Value).ToList();


                    //store in cache
                    await _cacheService.SetAsync<List<string>>(roleName, permissions);


                    //return result
                    if (permissions.Contains(requirement.PermissionName))
                    {
                        context.Succeed(requirement);
                        return;
                    }

                }

                else
                {
                    //found in cache

                    if (cachedPermissions != null && cachedPermissions.Contains(requirement.PermissionName))
                    {
                        context.Succeed(requirement);
                        return;
                    }
                }


            }
        }
    }
}

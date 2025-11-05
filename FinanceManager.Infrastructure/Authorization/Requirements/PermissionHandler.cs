using FinanceManager.Infrastructure.Authorization.ClaimTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace FinanceManager.Infrastructure.Authorization.Requirements
{


    public class PermissionHandler (RoleManager<IdentityRole> _roleManager , IDistributedCache _distributedCache): AuthorizationHandler<PermissionRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {

            // 1. Get role claims from JWT
            var roleClaims = context.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role).Select(c => c.Value).ToList();
            if (!roleClaims.Any()) return;
            

            // 2. Check each role for the required permission
            foreach (var roleName in roleClaims)

            {
                //check for role in redis
                var cacheKey =roleName; 
                string? cachedPermissions = await _distributedCache.GetStringAsync(cacheKey);

                if(string.IsNullOrEmpty(cachedPermissions))
                {
                    //not in cache, get from database
                    var role = await _roleManager.FindByNameAsync(roleName);
                    if (role == null) continue;

                    var claims = await _roleManager.GetClaimsAsync(role);
                    var permissions = claims.Where(c => c.Type == CustomClaimTypes.Permission).Select(c => c.Value).ToList();


                    //store in cache
                     var serializedPermissions  = JsonConvert.SerializeObject(permissions);
                    await _distributedCache.SetStringAsync(
                        cacheKey,
                        serializedPermissions

                        );


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
                    var permissions = JsonConvert.DeserializeObject<List<string>>(cachedPermissions);
                    if (permissions != null && permissions.Contains(requirement.PermissionName))
                    {
                        context.Succeed(requirement);
                        return;
                    }
                }


            }
        }
    }
}

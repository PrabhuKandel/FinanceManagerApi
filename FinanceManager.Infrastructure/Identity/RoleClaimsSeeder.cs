
using FinanceManager.Application.Common;
using FinanceManager.Infrastructure.Authorization;
using Microsoft.AspNetCore.Identity;
using  System.Security.Claims;

namespace FinanceManager.Infrastructure.Identity
{
    public static class RoleClaimsSeeder
    {
        public static async Task SeedRoleClaims(RoleManager<IdentityRole> roleManager)
        {
            // Ensure Admin role exists
            var adminRole = await roleManager.FindByNameAsync(RoleConstants.Admin);
            if (adminRole == null)
            {
                adminRole = new IdentityRole("Admin");
                await roleManager.CreateAsync(adminRole);
            }

            // Seed Admin claims (all permissions)
            var adminClaims = await roleManager.GetClaimsAsync(adminRole);
            var permissions = new[]
            {
            PermissionConstants.TransactionCategoryPermissions.ViewAll,
            PermissionConstants.TransactionCategoryPermissions.Create,
            PermissionConstants.TransactionCategoryPermissions.UpdateAll,
            PermissionConstants.TransactionCategoryPermissions.DeleteAll
        };

            foreach (var permission in permissions)
            {
                if (!adminClaims.Any(c => c.Type == permission))
                {
                    await roleManager.AddClaimAsync(adminRole, new Claim(permission, "true"));
                }
            }

            // Example: Normal User role
            var userRole = await roleManager.FindByNameAsync(RoleConstants.User);
            if (userRole == null)
            {
                userRole = new IdentityRole("User");
                await roleManager.CreateAsync(userRole);
            }

            var userClaims = await roleManager.GetClaimsAsync(userRole);
            var userPermissions = new[]
            {
            PermissionConstants.TransactionCategoryPermissions.ViewOwn,
            PermissionConstants.TransactionCategoryPermissions.Create,
            PermissionConstants.TransactionCategoryPermissions.UpdateOwn,
            PermissionConstants.TransactionCategoryPermissions.DeleteOwn
        };

            foreach (var permission in userPermissions)
            {
                if (!userClaims.Any(c => c.Type == permission))
                {
                    await roleManager.AddClaimAsync(userRole,new Claim(permission, "true"));
                }
            }
        }
    }
}

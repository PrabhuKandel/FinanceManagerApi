
using FinanceManager.Application.Common;
using FinanceManager.Application.Interfaces;
using FinanceManager.Infrastructure.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using  System.Security.Claims;

namespace FinanceManager.Infrastructure.Identity
{
    public static class RoleClaimsSeeder
    {
        public static async Task SeedRoleClaims(RoleManager<IdentityRole> roleManager, IApplicationDbContext context)
        {
            // Ensure Admin role exists
            var adminRole = await roleManager.FindByNameAsync(RoleConstants.Admin)?? new IdentityRole(RoleConstants.Admin);
            if (adminRole.Id == null) await roleManager.CreateAsync(adminRole);

          
            // Example: Normal User role
            var userRole = await roleManager.FindByNameAsync(RoleConstants.User)??new IdentityRole(RoleConstants.User);
            if (userRole.Id == null) await roleManager.CreateAsync(userRole);

            // Get all permissions
            var permissions = await context.Permissions.Where(p => p.IsActive).ToListAsync();

            // Assign all permissions to Admin
            var adminClaims = await roleManager.GetClaimsAsync(adminRole);
            foreach (var perm in permissions)
            {
                if (!adminClaims.Any(c => c.Type == perm.Name))
                    await roleManager.AddClaimAsync(adminRole, new Claim(perm.Name, "true"));
            }
            // Assign all permissions to User (for simplicity now)
            var userClaims = await roleManager.GetClaimsAsync(userRole);
            foreach (var perm in permissions)
            {
                if (!userClaims.Any(c => c.Type == perm.Name))
                    await roleManager.AddClaimAsync(userRole, new Claim(perm.Name, "true"));
            }
       
        }
    }
}

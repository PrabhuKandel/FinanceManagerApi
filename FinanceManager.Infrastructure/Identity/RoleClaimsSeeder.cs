
using FinanceManager.Application.Common;
using FinanceManager.Application.Interfaces;
using FinanceManager.Domain.Entities;
using FinanceManager.Infrastructure.Authorization;
using FinanceManager.Infrastructure.Authorization.ClaimTypes;
using FinanceManager.Infrastructure.Authorization.Permissions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using  System.Security.Claims;

namespace FinanceManager.Infrastructure.Identity
{
    public static class RoleClaimsSeeder
    {
        private static readonly Dictionary<string, IEnumerable<string>> RolePermissions = new()
        {
            [RoleConstants.Admin] = PermissionHelper.GetAdminPermissions(),

          
        };

        public static async Task SeedRoleClaimsAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (var (roleName, permissions) in RolePermissions)
            {
                var role = await EnsureRoleExistsAsync(roleManager, roleName);

                var existingClaims = await roleManager.GetClaimsAsync(role);
                foreach (var permission in permissions)
                {
                    if (!existingClaims.Any(c =>
                        c.Type == CustomClaimTypes.Permission && c.Value == permission))
                    {
                        await roleManager.AddClaimAsync(role,
                            new Claim(CustomClaimTypes.Permission, permission));
                    }
                }
            }
        }

        public static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
        {
            string FirstName = "Admin";
            String LastName = "User";
            String Address = " Admin Country";
            String adminEmail = "admin@gmail.com";
            String adminPassword = "Admin@123";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Address = Address,
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true


                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {

                    await userManager.AddToRoleAsync(adminUser, RoleConstants.Admin);

                }
            }
        }


        private static async Task<IdentityRole> EnsureRoleExistsAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role != null) return role;

            role = new IdentityRole(roleName);
            await roleManager.CreateAsync(role);
            return role;
        }
    }
}

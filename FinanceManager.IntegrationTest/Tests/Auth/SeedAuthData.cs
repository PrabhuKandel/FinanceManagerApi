using FinanceManager.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.IntegrationTest.Tests.Auth
{
    public static class SeedAuthData
    {
        public static async Task SeedAdminUserAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();


            // --- Ensure roles exist ---
            var roles = new[] { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }



            var adminEmail = "admin@gmail.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    FirstName = "Admin",
                    LastName = "User",
                    Address = " Admin Country",
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",



                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                {

                    await userManager.AddToRoleAsync(adminUser, "Admin");

                }
            }

                // --- Seed Normal user ---
                var normalEmail = "user@test.com";
                var normalUser = await userManager.FindByEmailAsync(normalEmail);
                if (normalUser == null)
                {
                    normalUser = new ApplicationUser
                    {
                        FirstName = "Normal",
                        LastName = "User",
                        UserName = normalEmail,
                        Email = normalEmail,
                        Address = "User Country"
                    };
                    var result = await userManager.CreateAsync(normalUser, "User@123");
                    if (result.Succeeded)
                        await userManager.AddToRoleAsync(normalUser, "User");
                }

            
        }
    }

}

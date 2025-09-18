using System;
using System.Collections.Generic;
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

            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));


            var adminUser = await userManager.FindByEmailAsync("admin@gmail.com");
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
        }
    }

}

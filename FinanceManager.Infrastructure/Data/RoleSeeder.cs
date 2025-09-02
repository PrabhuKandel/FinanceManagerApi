using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace FinanceManager.Infrastructure.Data
{
    public class RoleSeeder
    {
        public static List<string> Roles = new List<string>
        {
            "Admin",
            "User"
        };

        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
           
            foreach (var roleName in Roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }   
        }

        public static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
        {
            String FirstName = "Admin";
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

               var result =  await userManager.CreateAsync(adminUser,adminPassword );
                if (result.Succeeded)
                {
                  
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    
                }
            }



      
        }
    }
}

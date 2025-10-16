

using FinanceManager.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Infrastructure.Identity
{
    public static class IdentitySeedExtensions
    {
        public static async Task SeedIdentityDataAsync(
      this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Seed role claims
            await RoleClaimsSeeder.SeedRoleClaims(roleManager);

         
        }
    }
}



using System.Threading.Tasks;
using FinanceManager.Application.Interfaces;
using FinanceManager.Domain.Entities;
using FinanceManager.Infrastructure.Data.Seeder;
using Microsoft.AspNetCore.Identity;

namespace FinanceManager.Infrastructure.Data
{
    public static  class SeederExtensions
    {
        public static async Task SeedDataAsync(this IApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
           await TransactionRecordsSeeder.SeedFakeTransactionRecordsAsync(context, userManager);
        }
    }
}

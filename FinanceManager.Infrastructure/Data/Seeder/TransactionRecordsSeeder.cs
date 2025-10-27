

using FinanceManager.Application.Common;
using FinanceManager.Application.Interfaces;
using FinanceManager.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace FinanceManager.Infrastructure.Data.Seeder
{
    public static class TransactionRecordsSeeder
    {
        public static async Task SeedFakeTransactionRecordsAsync(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (context.TransactionRecords.Any()) return;

            var adminUsers = await userManager.GetUsersInRoleAsync(RoleConstants.Admin);
            var adminId = adminUsers.First().Id;

            var transactionCategoryIds = context.TransactionCategories.Select(c => c.Id).ToList();
            var applicationUserIds = context.ApplicationUsers.Select(u => u.Id).ToList();
            var paymentMethodIds = context.PaymentMethods.Select(p => p.Id).ToList();

            // Create fake data
            var transactionFaker = TransactionRecordFaker.Create(transactionCategoryIds, paymentMethodIds, applicationUserIds,adminId);
            var fakeTransactions = transactionFaker.Generate(10000);

            // Add transaction records including transaction payments
            await context.TransactionRecords.AddRangeAsync(fakeTransactions);
            await context.SaveChangesAsync();

        }
    }
}

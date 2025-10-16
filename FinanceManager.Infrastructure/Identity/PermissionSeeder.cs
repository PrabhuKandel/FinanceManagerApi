

using FinanceManager.Application.Interfaces;
using FinanceManager.Domain.Entities;
using FinanceManager.Infrastructure.Authorization;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Infrastructure.Identity
{
    public static class PermissionSeeder
    {
        public static async Task SeedPermissionsAsync(IApplicationDbContext dbContext)
        {
            if (await dbContext.Permissions.AnyAsync()) return;

            var permissions = new[]
            {
                new Permission { Name = PermissionConstants.TransactionCategoryPermissions.View, Description = "View transaction category", IsActive = true },
                new Permission { Name = PermissionConstants.TransactionCategoryPermissions.Create, Description = "Create transaction category", IsActive = true },
                new Permission { Name = PermissionConstants.TransactionCategoryPermissions.Update, Description = "Update transaction category", IsActive = true },
                new Permission { Name = PermissionConstants.TransactionCategoryPermissions.Delete, Description = "Delete transaction category", IsActive = true }
            };

            await dbContext.Permissions.AddRangeAsync(permissions);
            await dbContext.SaveChangesAsync();
        }
    }
}

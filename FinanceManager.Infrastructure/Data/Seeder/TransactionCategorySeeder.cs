using FinanceManager.Application.Interfaces;
using FinanceManager.Domain.Entities;

namespace FinanceManager.Infrastructure.Data.Seeder
{
    public static class TransactionCategorySeeder
    {
        public static async Task SeedTransactionCategoryAsync(IApplicationDbContext context)
        {
            if (context.TransactionCategories.Any()) return;
            context.TransactionCategories.AddRange(
                new TransactionCategory { Id = Guid.NewGuid(), Name = "Sales Revenue", Type = CategoryType.Income, Description = "Income from sales of goods or services" },
                new TransactionCategory { Id = Guid.NewGuid(), Name = "Service Revenue", Type = CategoryType.Income, Description = "Income from services provided" },
                new TransactionCategory { Id = Guid.NewGuid(), Name = "Other Income", Type = CategoryType.Income, Description = "Miscellaneous income" },
                new TransactionCategory { Id = Guid.NewGuid(), Name = "Salaries & Wages", Type = CategoryType.Expense, Description = "Payments to employees" },
                new TransactionCategory { Id = Guid.NewGuid(), Name = "Rent / Lease", Type = CategoryType.Expense, Description = "Office rent or equipment lease" },
                new TransactionCategory { Id = Guid.NewGuid(), Name = "Utilities", Type = CategoryType.Expense, Description = "Electricity, water, internet, phone" },
                new TransactionCategory { Id = Guid.NewGuid(), Name = "Office Supplies", Type = CategoryType.Expense, Description = "Stationery, office materials" },
                new TransactionCategory { Id = Guid.NewGuid(), Name = "Marketing & Advertising", Type = CategoryType.Expense, Description = "Promotional activities" },
                new TransactionCategory { Id = Guid.NewGuid(), Name = "Travel & Transport", Type = CategoryType.Expense, Description = "Business travel, fuel, vehicle costs" },
                new TransactionCategory { Id = Guid.NewGuid(), Name = "Professional Fees", Type = CategoryType.Expense, Description = "Consultancy, legal, or audit fees" },
                new TransactionCategory { Id = Guid.NewGuid(), Name = "Insurance", Type = CategoryType.Expense, Description = "Business insurance premiums" },
                new TransactionCategory { Id = Guid.NewGuid(), Name = "Miscellaneous Expenses", Type = CategoryType.Expense, Description = "Any other operational expenses" }
                );

            await context.SaveChangesAsync();
        }
    }
}

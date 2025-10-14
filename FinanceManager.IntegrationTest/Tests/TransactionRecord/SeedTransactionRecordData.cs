using FinanceManager.Domain.Entities;
using PaymentMethodEntity = FinanceManager.Domain.Entities.PaymentMethod;
using FinanceManager.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.IntegrationTest.Tests.TransactionRecord
{
    public static class SeedTransactionRecordData
    {
        /// <summary>
        /// Seed minimal data for Payment Methods and Transaction Categories.
        /// Idempotent: calling multiple times won't create duplicates.
        /// </summary>
        public static async Task SeedAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            // Seed TransactionCategories if not exists
            if (!context.TransactionCategories.Any())
            {
                context.TransactionCategories.AddRange(

                    new TransactionCategory
                    {
                        Id = Guid.NewGuid(),
                        Name = "Salary",
                        Description = "Monthly income from job",
                        Type = CategoryType.Income
                    },
                    new TransactionCategory
                    {
                        Id = Guid.NewGuid(),
                        Name = "Freelance",
                        Description = "Freelance or side income",
                        Type = CategoryType.Income
                    },
                    new TransactionCategory
                    {
                        Id = Guid.NewGuid(),
                        Name = "Food",
                        Description = "Groceries and dining out",
                        Type = CategoryType.Expense
                    },
                    new TransactionCategory
                    {
                        Id = Guid.NewGuid(),
                        Name = "Transport",
                        Description = "Bus, train, fuel, ride shares",
                        Type = CategoryType.Expense
                    },
                    new TransactionCategory
                    {
                        Id = Guid.NewGuid(),
                        Name = "Entertainment",
                        Description = "Movies, games, hobbies",
                        Type = CategoryType.Expense
                    }
                );
            }

            // Seed PaymentMethods if not exists
            if (!context.PaymentMethods.Any())
            {
                context.PaymentMethods.AddRange(
                          new PaymentMethodEntity
                          {
                              Id = Guid.NewGuid(),
                              Name = "Cash",
                              Description = "Cash payment",
                              IsActive = true
                          },
                          new PaymentMethodEntity
                          {
                              Id = Guid.NewGuid(),
                              Name = "Credit Card",
                              Description = "Payment via credit card",
                              IsActive = true
                          },
                          new PaymentMethodEntity
                          {
                              Id = Guid.NewGuid(),
                              Name = "Bank Transfer",
                              Description = "Payment via bank transfer",
                              IsActive = true
                          },
                          new PaymentMethodEntity
                          {
                              Id = Guid.NewGuid(),
                              Name = "UPI",
                              Description = "Unified Payment Interface",
                              IsActive = true
                          },
                          new PaymentMethodEntity
                          {
                              Id = Guid.NewGuid(),
                              Name = "PayPal",
                              Description = "Online PayPal payment",
                              IsActive = true
                          }

                );
            }

            context.SaveChanges();

        }
    }
}

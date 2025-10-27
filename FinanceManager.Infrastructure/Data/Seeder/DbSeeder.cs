using FinanceManager.Domain.Entities;

namespace FinanceManager.Infrastructure.Data.Seeder
{
    public class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
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
                          new PaymentMethod
                          {
                              Id = Guid.NewGuid(),
                              Name = "Cash",
                              Description = "Cash payment",
                              IsActive = true
                          },
                          new PaymentMethod
                          {
                              Id = Guid.NewGuid(),
                              Name = "Credit Card",
                              Description = "Payment via credit card",
                              IsActive = true
                          },
                          new PaymentMethod
                          {
                              Id = Guid.NewGuid(),
                              Name = "Bank Transfer",
                              Description = "Payment via bank transfer",
                              IsActive = true
                          },
                          new PaymentMethod
                          {
                              Id = Guid.NewGuid(),
                              Name = "UPI",
                              Description = "Unified Payment Interface",
                              IsActive = true
                          },
                          new PaymentMethod
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

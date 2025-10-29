using FinanceManager.Application.Interfaces;
using FinanceManager.Domain.Entities;

namespace FinanceManager.Infrastructure.Data.Seeder
{
    public static class PaymentMethodSeeder
    {
        public static async Task SeedPaymentMethodAsync(IApplicationDbContext context)
        {
            if (context.PaymentMethods.Any()) return;
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
              Name = "Khalti",
              Description = "Khalti",
              IsActive = true
          },

          new PaymentMethod
          {
              Id = Guid.NewGuid(),
              Name = "Esewa",
              Description = "Esewa",
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

            await context.SaveChangesAsync();

        }
    }
}

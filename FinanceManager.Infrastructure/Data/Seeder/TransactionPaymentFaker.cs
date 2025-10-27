
using Bogus;
using FinanceManager.Domain.Entities;

namespace FinanceManager.Infrastructure.Data.Seeder
{
    public static class TransactionPaymentFaker
    {
        public static Faker<TransactionPayment> Create(Guid transactionRecordId, List<Guid> paymentMethodIds)
        {
            return new Faker<TransactionPayment>()
                .RuleFor(tp => tp.Id, _ => Guid.NewGuid())
                .RuleFor(tp => tp.TransactionRecordId, _ => transactionRecordId)
                .RuleFor(tp => tp.PaymentMethodId, f => f.PickRandom(paymentMethodIds))
                .RuleFor(tp => tp.Amount, f => Math.Round(f.Random.Decimal(50, 5000), 2));
        }
    }
}

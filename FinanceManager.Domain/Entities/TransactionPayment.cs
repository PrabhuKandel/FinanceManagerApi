

namespace FinanceManager.Domain.Entities
{
    public class TransactionPayment
    {
        public Guid Id { get; set; }
        public Guid TransactionRecordId { get; set; }
        public TransactionRecord? TransactionRecord { get; set; }
        public Guid PaymentMethodId { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public decimal Amount { get; set; }    // partial amount

    }
}

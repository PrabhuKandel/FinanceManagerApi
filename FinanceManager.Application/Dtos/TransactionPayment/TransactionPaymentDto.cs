

namespace FinanceManager.Application.Dtos.TransactionPayment
{
    public class TransactionPaymentDto
    {
        public Guid PaymentMethodId { get; set; }
        public decimal Amount { get; set; }
    }
}

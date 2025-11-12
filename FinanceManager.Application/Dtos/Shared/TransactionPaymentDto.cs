namespace FinanceManager.Application.Dtos.Shared
{
    public class TransactionPaymentDto
    {
        public Guid PaymentMethodId { get; set; }
        public decimal Amount { get; set; }
    }
}

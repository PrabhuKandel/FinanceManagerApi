
namespace FinanceManager.Application.FeaturesDapper.Reports.Dtos
{
    public class TransactionRecordSummaryByPaymentMethodDto
    {
        public required string PaymentMethodName { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalTransactions { get; set; }
    }
}

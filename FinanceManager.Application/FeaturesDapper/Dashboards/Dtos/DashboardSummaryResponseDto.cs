
namespace FinanceManager.Application.FeaturesDapper.Dashboards.Dtos
{
    public class DashboardSummaryResponseDto
    {
        public int TotalTransactions { get; set; }
        public int TotalTransactionCategories { get; set; }
        public int TotalPaymentMethods { get; set; }
        public int ActivePaymentMethods { get; set; }
        public int InactivePaymentMethods { get; set; }
    
    }
}

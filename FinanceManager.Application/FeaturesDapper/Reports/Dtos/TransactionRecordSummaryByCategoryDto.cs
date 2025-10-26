

namespace FinanceManager.Application.FeaturesDapper.Reports.Dtos
{
    public class TransactionRecordSummaryByCategoryDto
    {
        public Guid TransactionCategoryId { get; set; }
        public required string TransactionCategoryName { get; set; }

        public required string CategoryType { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalTransactions { get; set; }
    }
}


namespace FinanceManager.Application.Dtos.Report
{
    public class TransactionRecordSummaryByCategoryDto
    {
        public Guid TransactionCategoryId { get; set; }
        public string? TransactionCategoryName { get; set; }
        public decimal TotalAmount { get; set; }
        public int TransactionRecordCount { get; set; }
    }
}

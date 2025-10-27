

namespace FinanceManager.Application.FeaturesDapper.Reports.Dtos
{
    public class TransactionRecordSummaryByCategoryTypeDto
    {
        public  required string PeriodStart { get; set; }
        public required string PeriodEnd { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }

        public decimal NetBalance { get; set; }
    }
}

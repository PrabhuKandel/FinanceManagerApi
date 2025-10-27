

namespace FinanceManager.Application.FeaturesDapper.Reports.Dtos
{
    public class TransactionRecordSummaryByCategoryTypeDto
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }

        public decimal NetBalance { get; set; }
    }
}

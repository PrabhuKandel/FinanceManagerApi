

using FinanceManager.Domain.Enums;

namespace FinanceManager.Application.FeaturesDapper.Reports.Dtos
{
    public class TransactionCategoryBudgetVsActualOutflowDto
    {

        public required string TransactionCategoryName { get; set; }

        public PeriodType PeriodType { get; set; }

        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public decimal BudgetAmount { get; set; }
        public decimal ActualAmount { get; set; }

        public decimal RemainingBudget { get; set; }

        public decimal BudgetUsagePercent { get; set; }
    }
}

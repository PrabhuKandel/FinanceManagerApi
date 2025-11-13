

using FinanceManager.Domain.Enums;

namespace FinanceManager.Application.Features.Budgets.Dtos
{
    public class BudgetResponseDto
    {
        public Guid Id { get; set; }
        public string TransactionCategoryName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string PeriodType { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
    }
}

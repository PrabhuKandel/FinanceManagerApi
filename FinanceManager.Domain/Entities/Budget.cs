
using FinanceManager.Domain.Enums;

namespace FinanceManager.Domain.Entities
{
    public class Budget
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid TransactionCategoryId { get; set; }
        public TransactionCategory? TransactionCategory { get; set; }

        public decimal Amount { get; set; }

        public DateTime PeriodStart { get; set; }

        public DateTime PeriodEnd { get; set; }

        public BudgetApprovalStatus Status { get; set; } = BudgetApprovalStatus.Pending;

        public bool IsActive { get; set; } = true;  

        public required string CreatedByApplicationUserId { get; set; }

        public ApplicationUser? CreatedByApplicationUser { get; set; }


        public string? UpdatedByApplicationUserId { get; set; }
        public ApplicationUser? UpdatedByApplicationUser { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

            
        public string? ReviewedByApplicationUserId { get; set; }
        public ApplicationUser? ReviewedByApplicationUser { get; set; }

        public DateTime? ReviewedAt { get; set; }


    }
}

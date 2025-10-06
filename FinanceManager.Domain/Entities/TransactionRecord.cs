using System.ComponentModel.DataAnnotations;
using FinanceManager.Domain.Enums;

namespace FinanceManager.Domain.Entities
{
    public class TransactionRecord
    {
  
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TransactionCategoryId { get; set; }
        public  TransactionCategory? TransactionCategory { get; set; }

        public decimal Amount { get; set; }
        public string? Description { get; set; }

        public DateTime TransactionDate { get; set; }
        // Created By User
        public  required string CreatedByApplicationUserId { get; set; }
        public ApplicationUser? CreatedByApplicationUser { get; set; }

        // Updated By User 
        public string? UpdatedByApplicationUserId { get; set; } = null;
        public ApplicationUser? UpdatedByApplicationUser { get; set; }

        public ICollection<TransactionPayment> TransactionPayments { get; set; } = new List<TransactionPayment>();

        public TransactionRecordApprovalStatus ApprovalStatus { get; set; } = TransactionRecordApprovalStatus.Pending;


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

      
        public DateTime UpdatedAt { get; set; }


    }
}

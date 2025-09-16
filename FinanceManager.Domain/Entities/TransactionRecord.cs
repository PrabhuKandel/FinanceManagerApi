using System.ComponentModel.DataAnnotations;

namespace FinanceManager.Domain.Entities
{
    public class TransactionRecord
    {
  
        public Guid Id { get; set; } = Guid.NewGuid();


        // Created By User
        public string CreatedByApplicationUserId { get; set; }
        public ApplicationUser CreatedByApplicationUser { get; set; }

        // Updated By User 
        public string UpdatedByApplicationUserId { get; set; }
        public ApplicationUser UpdatedByApplicationUser { get; set; }

        public Guid TransactionCategoryId{ get; set; }
        public TransactionCategory TransactionCategory { get; set; }

        public decimal Amount { get; set; }

   
        public Guid PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public string? Description { get; set; }

  
        public DateTime TransactionDate { get; set; }
   
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

      
        public DateTime UpdatedAt { get; set; }


    }
}

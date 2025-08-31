using System.ComponentModel.DataAnnotations;

namespace FinanceManager.Domain.Models
{
    public class TransactionRecord
    {
  
        public Guid Id { get; set; } = Guid.NewGuid();

        
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

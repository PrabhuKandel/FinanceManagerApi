using System.ComponentModel.DataAnnotations;

namespace FinanceManager.Domain.Models
{
    public class TransactionRecord
    {
        [Key]
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Transaction category is required.")]
        public Guid TransactionCategoryId{ get; set; }
        public TransactionCategory TransactionCategory { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Payment method is required.")]
        public Guid PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Transaction date is required.")]
        public DateTime TransactionDate { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime UpdatedAt { get; set; }


    }
}

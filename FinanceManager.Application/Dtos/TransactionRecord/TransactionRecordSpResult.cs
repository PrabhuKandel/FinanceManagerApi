
namespace FinanceManager.Application.Dtos.TransactionRecord
{
    public class TransactionRecordSpResult
    {
        public Guid TransactionRecordId { get; set; }
        public Guid TransactionCategoryId { get; set; }
        public string TransactionCategoryName { get; set; } = string.Empty;
        public Guid PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public  required string CreatedByUserId { get; set; }
        public string CreatedByFirstName { get; set; } = string.Empty;
        public required string UpdatedByUserId { get; set; }
        public string UpdatedByFirstName { get; set; } = string.Empty;
    }
}



namespace FinanceManager.Application.Dtos.TransactionRecord
{
    public class TransactionRecordPatchDto
    {
        
        public Guid? TransactionCategoryId { get; set; }

        public decimal? Amount { get; set; }

        public Guid? PaymentMethodId { get; set; }

        public string? Description { get; set; }
        public DateTime? TransactionDate { get; set; }
    }
}

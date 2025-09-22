

using FinanceManager.Application.Dtos.TransactionPayment;

namespace FinanceManager.Application.Dtos.TransactionRecord
{
    public class TransactionRecordPatchDto
    {
        
        public Guid? TransactionCategoryId { get; set; }

        public decimal? Amount { get; set; }

        public string? Description { get; set; }
        public List<TransactionPaymentDto> Payments { get; set; } = new List<TransactionPaymentDto>();
        public DateTime? TransactionDate { get; set; }
    }
}

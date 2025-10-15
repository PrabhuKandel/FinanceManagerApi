

namespace FinanceManager.Application.Features.TransactionRecords.Dtos
{
   
        public class TransactionRecordExportDto
        {
        public DateTime TransactionDate { get; set; }
        public  required string Category { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public  required string? ApprovalStatus { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? ActionedBy { get; set; }
        public List<PaymentExportDto> Payments { get; set; } = new();
        public string CreatedByApplicationUserId { get; set; } = default!;
    }

    public class PaymentExportDto
    {
        public string? PaymentName { get; set; }
        public decimal Amount { get; set; }
    }

}

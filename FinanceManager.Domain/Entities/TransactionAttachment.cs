

namespace FinanceManager.Domain.Entities
{
    public class TransactionAttachment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string FileName { get; set; }
        public required string FilePath { get; set; }

        public required string FileType { get; set; }
        public required Guid TransactionRecordId { get; set; }
        public TransactionRecord? TransactionRecord { get; set; }

        public required string UploadedByApplicationUserId { get; set; }
        public ApplicationUser? UploadedByApplicationUser { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
    }
}

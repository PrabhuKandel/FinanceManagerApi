
namespace FinanceManager.Domain.Enums
{
    public enum TransactionRecordApprovalStatus
    {
        Pending = 0,    // Waiting for admin review
        Approved = 1,   // Verified by admin
        Cancelled = 2   // Invalid and cancelled by admin
    }
}

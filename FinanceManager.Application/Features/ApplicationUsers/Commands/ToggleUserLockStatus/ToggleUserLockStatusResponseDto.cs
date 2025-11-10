
namespace FinanceManager.Application.Features.ApplicationUsers.Commands.ToggleUserLockStatus
{
    public class ToggleUserLockStatusResponseDto
    {
        public required string UserId { get; set; } 
        public bool IsLocked { get; set; }
        public bool IsManuallyLocked { get; set; }
        public string? LockReason { get; set; }
    }
}

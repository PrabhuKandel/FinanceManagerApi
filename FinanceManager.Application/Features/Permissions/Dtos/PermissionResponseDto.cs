

namespace FinanceManager.Application.Features.Permissions.Dtos
{
    public class PermissionResponseDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; } 

        public bool IsActive { get; set; }
    }
}

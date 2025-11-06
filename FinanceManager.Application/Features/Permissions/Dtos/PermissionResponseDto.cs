

namespace FinanceManager.Application.Features.Permissions.Dtos
{
    public class RolePermissionsResponseDto
    {
        public required string RoleId { get; set; }

        public required string RoleName { get; set; }
        public List<string> Permissions { get; set; }  = new List<string>();


    }
}



using FinanceManager.Application.Common;
using MediatR;

namespace FinanceManager.Application.Features.Roles.Commands.AssignPermissions
{
    public record AssignPermissionsToRoleCommand
        (
        string RoleId,
        List<Guid> PermissionIds
        ) :IRequest<OperationResult<string>>
    {
    }
}

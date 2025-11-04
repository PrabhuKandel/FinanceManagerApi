

using FinanceManager.Application.Common;
using MediatR;

namespace FinanceManager.Application.Features.Roles.Commands.Create
{
    public record CreateRoleCommand
        (
            string RoleName,
            IEnumerable<string> Permissions
        ):IRequest<OperationResult<string>>
    {
    }
}

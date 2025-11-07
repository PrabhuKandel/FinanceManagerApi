using FinanceManager.Application.Common;
using MediatR;

namespace FinanceManager.Application.Features.ApplicationUsers.Commands.AssignRole
{
    public record AssignRoleCommand
    (
        string ApplicationUserId,
        List<string> RoleNames
    ):IRequest<OperationResult<string>>
    {
    }
}

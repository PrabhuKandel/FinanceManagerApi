using FinanceManager.Application.Common;
using MediatR;

namespace FinanceManager.Application.Features.ApplicationUsers.Commands.ToggleUserLockStatus
{
    public record ToggleUserLockStatusCommand
        (
        string UserId
        ) :IRequest<OperationResult<string>>
    {
    }
}

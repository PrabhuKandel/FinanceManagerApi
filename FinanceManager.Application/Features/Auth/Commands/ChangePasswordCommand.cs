using FinanceManager.Application.Common;
using MediatR;

namespace FinanceManager.Application.Features.Auth.Commands
{
    public record ChangePasswordCommand ( string CurrentPassword, string NewPassword) : IRequest<OperationResult<string>>
    {
    }
}

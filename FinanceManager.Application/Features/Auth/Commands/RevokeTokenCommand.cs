

using FinanceManager.Application.Common;
using MediatR;

namespace FinanceManager.Application.Features.Auth.Commands
{
    public record RevokeTokenCommand(string Token):IRequest<OperationResult<string>>
    {
    }
}

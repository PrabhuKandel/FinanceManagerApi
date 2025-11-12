using FinanceManager.Application.Common;
using MediatR;

namespace FinanceManager.Application.Features.Auth.Commands.RevokeToken
{
    public record RevokeTokenCommand(string Token, string? RevocationReason):IRequest<OperationResult<string>>
    {
    }
}

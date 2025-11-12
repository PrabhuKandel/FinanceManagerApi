using FinanceManager.Application.Common;
using FinanceManager.Application.Features.Auth.Dtos;
using MediatR;

namespace FinanceManager.Application.Features.Auth.Commands.RefreshTokens
{
    public record RefreshTokenCommand(string RefreshToken) : IRequest<OperationResult<TokenResponseDto>>
    {
    }
}

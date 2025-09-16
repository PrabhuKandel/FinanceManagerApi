using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.ApplicationUser;
using MediatR;

namespace FinanceManager.Application.Features.Auth.Commands
{
    public record RefreshTokenCommand(string RefreshToken) : IRequest<OperationResult<TokenResponseDto>>
    {
    }
}

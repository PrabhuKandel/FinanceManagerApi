using FinanceManager.Application.Common;
using FinanceManager.Application.Features.Auth.Dtos;
using MediatR;

namespace FinanceManager.Application.Features.Auth.Commands.ApplicationUserLogin
{
    public record ApplicationUserLoginCommand
        (
        
        string Email,
        string Password
        
        ):IRequest<OperationResult<ApplicationUserLoginResponseDto>>
    {
    }
}



using FinanceManager.Application.Common;
using MediatR;

namespace FinanceManager.Application.Features.Auth.ForgotPassword.Commands
{
    public record ForgotPasswordCommand
        (
            string Email,
            string ClientURI
        ) : IRequest<OperationResult<string>>
    {
     
    }
}

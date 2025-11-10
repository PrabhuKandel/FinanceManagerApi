

using FinanceManager.Application.Common;
using MediatR;

namespace FinanceManager.Application.Features.Auth.ResetPassword.Commands
{
    public record ResetPasswordCommand
        (
            string Email,
            string Token,
            string NewPassword
        ):IRequest<OperationResult<string>>
    {
    }
}

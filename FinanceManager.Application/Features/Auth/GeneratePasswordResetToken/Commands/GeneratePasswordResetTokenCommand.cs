

using FinanceManager.Application.Common;
using MediatR;

namespace FinanceManager.Application.Features.Auth.GeneratePasswordResetToken.Commands
{
    public record GeneratePasswordResetToken
        (
            string Email,
            string ClientURI
        ) : IRequest<OperationResult<string>>
    {
     
    }
}

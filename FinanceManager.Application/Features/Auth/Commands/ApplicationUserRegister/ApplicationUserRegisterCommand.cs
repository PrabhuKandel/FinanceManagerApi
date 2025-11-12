using FinanceManager.Application.Common;
using MediatR;

namespace FinanceManager.Application.Features.Auth.Commands.ApplicationUserRegister
{
    public record ApplicationUserRegisterCommand
        (
           string FirstName,
            string LastName,
            string Address,
            string Email,
            string? RoleId
        ) :IRequest<OperationResult<string>>
    {

    }
}

using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.ApplicationUser;
using MediatR;

namespace FinanceManager.Application.Features.ApplicationUsers.Commands.UpdateApplicationUser
{
    public record UpdateApplicationUserCommand(string Id, string FirstName, string LastName, string Address, string Email, string RoleId) : IRequest<OperationResult<ApplicationUserResponseDto>>
    {
    }
}

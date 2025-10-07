

using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.ApplicationUser;
using MediatR;

namespace FinanceManager.Application.Features.ApplicationUsers.Queries.GetApplicationUserById
{
    public record GetApplicationUserByIdQuery(string Id): IRequest<OperationResult<ApplicationUserResponseDto>>
    {
    }
}

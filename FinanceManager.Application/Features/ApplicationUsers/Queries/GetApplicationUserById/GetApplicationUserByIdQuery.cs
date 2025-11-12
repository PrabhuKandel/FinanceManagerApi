

using FinanceManager.Application.Common;
using FinanceManager.Application.Features.ApplicationUsers.Dtos;
using MediatR;

namespace FinanceManager.Application.Features.ApplicationUsers.Queries.GetApplicationUserById
{
    public record GetApplicationUserByIdQuery(string Id): IRequest<OperationResult<ApplicationUserResponseDto>>
    {
    }
}

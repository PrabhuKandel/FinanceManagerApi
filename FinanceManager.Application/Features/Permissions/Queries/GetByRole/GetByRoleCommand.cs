

using FinanceManager.Application.Common;
using FinanceManager.Application.Features.Permissions.Dtos;
using MediatR;

namespace FinanceManager.Application.Features.Permissions.Queries.GetByRole
{
    public record GetByRoleCommand( String RoleId) : IRequest<OperationResult<RolePermissionsResponseDto>>   
    {
    }
}

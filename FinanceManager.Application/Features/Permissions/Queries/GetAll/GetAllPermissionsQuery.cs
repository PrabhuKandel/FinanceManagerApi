

using FinanceManager.Application.Common;
using FinanceManager.Application.Features.Permissions.Dtos;
using MediatR;

namespace FinanceManager.Application.Features.Permissions.Queries.GetAll
{
    public class GetAllPermissionsQuery:IRequest<OperationResult<IEnumerable<PermissionResponseDto>>>
    {
    }
}

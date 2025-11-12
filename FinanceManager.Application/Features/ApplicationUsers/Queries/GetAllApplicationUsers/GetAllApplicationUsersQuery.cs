

using FinanceManager.Application.Common;
using FinanceManager.Application.Features.ApplicationUsers.Dtos;
using MediatR;

namespace FinanceManager.Application.Features.ApplicationUsers.Queries.GetAllApplicationUsers
{
    public class GetAllApplicationUsersQuery : IRequest<OperationResult<IEnumerable<ApplicationUserResponseDto>>>
    {

    }
}

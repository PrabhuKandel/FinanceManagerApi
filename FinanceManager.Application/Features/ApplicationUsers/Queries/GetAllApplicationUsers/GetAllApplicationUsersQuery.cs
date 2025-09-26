

using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.ApplicationUser;
using MediatR;

namespace FinanceManager.Application.Features.ApplicationUsers.Queries.GetAllApplicationUsers
{
    public class GetAllApplicationUsersQuery : IRequest<OperationResult<IEnumerable<ApplicationUserResponseDto>>>
    {

    }
}

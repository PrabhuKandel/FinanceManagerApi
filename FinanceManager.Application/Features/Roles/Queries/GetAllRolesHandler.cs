
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.Roles;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.Roles.Queries
{
    public class GetAllRolesHandler(RoleManager<IdentityRole> roleManager) : IRequestHandler<GetAllRolesQuery, OperationResult<IEnumerable<RoleResponseDto>>>
    {
        public async Task<OperationResult<IEnumerable<RoleResponseDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await roleManager.Roles
                                   .Select(r => new RoleResponseDto
                                   {
                                       Id = r.Id,
                                       Name = r.Name!
                                   })
                                   .ToListAsync(cancellationToken);

            return new OperationResult<IEnumerable<RoleResponseDto>>
            {

                Data = roles,
                Message = "Roles retrieved successfully."
            };
        }
    }
}


using FinanceManager.Application.Common;
using FinanceManager.Application.Features.Roles.Dtos;
using FinanceManager.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.Roles.Queries
{
    public class GetAllRolesHandler(RoleManager<IdentityRole> roleManager, IApplicationDbContext _context) : IRequestHandler<GetAllRolesQuery, OperationResult<IEnumerable<RoleResponseDto>>>
    {
        public async Task<OperationResult<IEnumerable<RoleResponseDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            //var roles = await roleManager.Roles
            //                       .Select(r => new RoleResponseDto
            //                       {
            //                           Id = r.Id,
            //                           Name = r.Name!,


            //                       })
            //                       .ToListAsync(cancellationToken);

            var roleDtos  = await (
                
                from r in roleManager.Roles
                join rc in _context.RoleClaims.Where(c => c.ClaimType == "permission")
                on r.Id equals rc.RoleId into roleClaimsGroup
                select new RoleResponseDto
                {
                    Id = r.Id,
                    Name = r.Name!,
                    Permissions = roleClaimsGroup.Select(c => c.ClaimValue).ToList()
                }
                 ).ToListAsync(cancellationToken);

            return new OperationResult<IEnumerable<RoleResponseDto>>
            {

                Data = roleDtos,
                Message = "Roles retrieved successfully."
            };
        }
    }
}

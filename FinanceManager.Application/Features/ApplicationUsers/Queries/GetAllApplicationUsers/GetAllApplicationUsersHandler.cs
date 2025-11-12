using FinanceManager.Application.Common;
using FinanceManager.Application.Features.ApplicationUsers.Dtos;
using FinanceManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.ApplicationUsers.Queries.GetAllApplicationUsers
{
    public class GetAllApplicationUsersHandler(IApplicationDbContext context) : IRequestHandler<GetAllApplicationUsersQuery, OperationResult<IEnumerable<ApplicationUserResponseDto>>>
    {
        public async Task<OperationResult<IEnumerable<ApplicationUserResponseDto>>> Handle(GetAllApplicationUsersQuery request, CancellationToken cancellationToken)
        {

            var applicationUsers = await context.ApplicationUsers
                .Select(u => new ApplicationUserResponseDto
                {
                   Id =  u.Id,
                   Email= u.Email!,
                   FirstName= u.FirstName,
                   LastName= u.LastName,
                   Address = u.Address,
                   Roles = (from userRole in context.UserRoles
                             join role in context.Roles on userRole.RoleId equals role.Id
                             where userRole.UserId == u.Id
                             select role.Name).ToList(),
                   IsLocked = u.LockoutEnabled && u.LockoutEnd.HasValue && u.LockoutEnd > DateTimeOffset.UtcNow,
                   IsManuallyLocked = u.IsManuallyLocked,
                   LockReason = u.LockReason??"Temporarily Locked"



                }).ToListAsync(cancellationToken);

            return new OperationResult<IEnumerable<ApplicationUserResponseDto>>
            {
                Message = "Users fetched successfully",
                Data = applicationUsers
            };
            
        }
    }
}

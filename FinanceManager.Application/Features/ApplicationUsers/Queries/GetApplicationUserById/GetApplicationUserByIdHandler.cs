
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.ApplicationUser;
using FinanceManager.Application.Interfaces;
using FinanceManager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.ApplicationUsers.Queries.GetApplicationUserById
{
    public class GetApplicationUserByIdHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager) : IRequestHandler<GetApplicationUserByIdQuery, OperationResult<ApplicationUserResponseDto>>
    {
        public async Task<OperationResult<ApplicationUserResponseDto>> Handle(GetApplicationUserByIdQuery request, CancellationToken cancellationToken)
        {

            var applicationUser = await context.ApplicationUsers
                .Where(u=>u.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            var roles = await userManager.GetRolesAsync(applicationUser);

            // Map to DTO
            var dto = new ApplicationUserResponseDto
            {
                Id = applicationUser.Id,
                Email = applicationUser.Email!,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                Address = applicationUser.Address,
                Roles = roles.ToList() 
            };



            return new OperationResult<ApplicationUserResponseDto>
            {
                Message = "User fetched successfully",
                Data = dto
            };
        }
    }
}

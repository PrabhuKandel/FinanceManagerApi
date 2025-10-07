using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.ApplicationUser;
using FinanceManager.Application.Interfaces;
using FinanceManager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.ApplicationUsers.Commands.UpdateApplicationUser
{
    public class UpdateApplicationUserHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : IRequestHandler<UpdateApplicationUserCommand, OperationResult<ApplicationUserResponseDto>>
    {
        public async Task<OperationResult<ApplicationUserResponseDto>> Handle(UpdateApplicationUserCommand request, CancellationToken cancellationToken)
        {
            var applicationUser = await context.ApplicationUsers
                     .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            var roleEntity = await roleManager.FindByIdAsync(request.RoleId);

            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

            applicationUser!.FirstName = request.FirstName;
            applicationUser.LastName = request.LastName;
            applicationUser.Address = request.Address;
            applicationUser.Email = request.Email;

            // Update role if changed
            var currentRoles = await userManager.GetRolesAsync(applicationUser);

            if (!currentRoles.Contains(roleEntity.Name))
            {
                await userManager.RemoveFromRolesAsync(applicationUser, currentRoles);
                var role = await userManager.AddToRoleAsync(applicationUser, roleEntity.Name);
            }
            await context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            // Fetch updated roles
            var roles = await userManager.GetRolesAsync(applicationUser);

            // Return updated DTO
            var dto = new ApplicationUserResponseDto
            {
                Id = applicationUser.Id,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                Address = applicationUser.Address,
                Email = applicationUser.Email!,
                Roles = roles.ToList()
            };

            return new OperationResult<ApplicationUserResponseDto>
            {
                Message = "User updated successfully",
                Data = dto
            };


        }
    }
}

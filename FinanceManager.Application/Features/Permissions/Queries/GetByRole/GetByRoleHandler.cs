using Ardalis.GuardClauses;
using FinanceManager.Application.Common;
using FinanceManager.Application.Features.Permissions.Dtos;
using FinanceManager.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.Permissions.Queries.GetByRole
{
    public class GetByRoleHandler(RoleManager<IdentityRole> roleManager, IApplicationDbContext _context) : IRequestHandler<GetByRoleCommand, OperationResult<RolePermissionsResponseDto>>
    {
        public async Task<OperationResult<RolePermissionsResponseDto>> Handle(GetByRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await roleManager.FindByIdAsync(request.RoleId);
            Guard.Against.Null(role, message: $"Role with ID {request.RoleId} not found.");


            //get all permissions claim for that role
            var permissionClaims = await _context.RoleClaims
                .Where(rc => rc.RoleId == request.RoleId && rc.ClaimType == "Permission")
                .Select(rc=>rc.ClaimValue)
                .ToListAsync(cancellationToken);

            var rolePermissions = new RolePermissionsResponseDto
            {
                RoleId = request.RoleId,
                RoleName = role!.Name!,
                Permissions = permissionClaims
            };

            return new OperationResult <RolePermissionsResponseDto>
            {
                Data = rolePermissions ,
                Message = "Permissions retrieved successfully."
            };


        }
    }
}

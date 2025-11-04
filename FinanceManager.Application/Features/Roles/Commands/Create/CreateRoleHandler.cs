

using System.Security.Claims;
using FinanceManager.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FinanceManager.Application.Features.Roles.Commands.Create
{
    public class CreateRoleHandler(RoleManager<IdentityRole> _roleManager) : IRequestHandler<CreateRoleCommand, OperationResult<string>>
    {
        public async Task<OperationResult<string>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {

            var role = await _roleManager.FindByNameAsync(request.RoleName);

            // 2. Create role if it does not exist
            if (role == null)
            {
                role = new IdentityRole(request.RoleName);
                 await _roleManager.CreateAsync(role);
         
            }

            var existingClaims = await _roleManager.GetClaimsAsync(role);

            foreach (var permission in request.Permissions)
            {
                if (!existingClaims.Any(c => c.Type == "Permission" && c.Value == permission))
                {
                    await _roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }

            return new OperationResult<string> { Message = "Role created successfully"};
        }
    }
}

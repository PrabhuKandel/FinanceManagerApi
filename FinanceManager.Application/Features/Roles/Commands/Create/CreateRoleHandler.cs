
 using System.Security.Claims;
using FinanceManager.Application.Common;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FinanceManager.Application.Features.Roles.Commands.Create
{
    public class CreateRoleHandler(RoleManager<IdentityRole> _roleManager) : IRequestHandler<CreateRoleCommand, OperationResult<string>>
    {
        public async Task<OperationResult<string>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {

            var role = await _roleManager.FindByNameAsync(request.RoleName);
            if(role!=null) throw new BusinessValidationException("Role already exists");

            //  Create role if it does not exist

                role = new IdentityRole(request.RoleName);
                 await _roleManager.CreateAsync(role);


            foreach (var permission in request.Permissions.Distinct())
            {
                
                    await _roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                
            }

      

            return new OperationResult<string> { Message = "Role created successfully"};
        }
    }
}

using Ardalis.GuardClauses;
using DocumentFormat.OpenXml.Spreadsheet;
using FinanceManager.Application.Common;
using FinanceManager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace FinanceManager.Application.Features.ApplicationUsers.Commands.AssignRole
{
    public class AssignRoleHandler(UserManager<ApplicationUser> _userManager) : IRequestHandler<AssignRoleCommand, OperationResult<string>>
    {
        public async Task<OperationResult<string>> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
           var applicationUserFromDb = await _userManager.FindByIdAsync(request.ApplicationUserId);
           Guard.Against.Null(nameof(applicationUserFromDb),"User not found");

            var currentRoles = await _userManager.GetRolesAsync(applicationUserFromDb!);

            // Remove all current roles
            if (currentRoles.Any())
                 await _userManager.RemoveFromRolesAsync(applicationUserFromDb!, currentRoles);

            // Add new roles from request
             await _userManager.AddToRolesAsync(applicationUserFromDb!, request.RoleNames);


            return new OperationResult<string>
            {
                Message = "Role Assgined Successfully"
            };
        }
    }
}

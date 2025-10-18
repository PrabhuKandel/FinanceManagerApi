using Microsoft.AspNetCore.Mvc;
using FinanceManager.Application.Features.Roles.Queries;
using MediatR;
using FinanceManager.Application.Features.Roles.Commands.AssignPermissions;
using Microsoft.AspNetCore.Authorization;
using FinanceManager.Infrastructure.Authorization.Permissions;
namespace FinanceManager.Api.Controllers
{
    [Route("api/roles")]
    [ApiController]
    [Authorize]
    public class RolesController(ISender sender) : ControllerBase
    {
        [Authorize(Policy = PermissionConstants.RolePermissions.View)]
        [HttpGet("get-all")]
        public async  Task<IActionResult> GetAll()
        {
            var response= await sender.Send(new GetAllRolesQuery());
            return Ok(response);
        }


        [HttpPost("{roleId}/assign-permissions")]
        public async Task<IActionResult> AssignPermissions(AssignPermissionsToRoleCommand command)
        {


           var response =  await sender.Send(command);
            return Ok(response);
        }
    }
}

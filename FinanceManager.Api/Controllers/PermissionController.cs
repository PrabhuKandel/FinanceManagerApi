using ClosedXML.Excel;
using FinanceManager.Application.Features.Permissions.Queries.GetByRole;
using FinanceManager.Infrastructure.Authorization.Permissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Api.Controllers
{
    [Route("api/permissions")]
    [ApiController]
    public class PermissionController(ISender _sender) : ControllerBase
    {
        [Authorize(Policy =PermissionConstants.Permission.View)]
        [HttpGet("get-all")]
        public  IActionResult GetAll()
        {
            var response = PermissionHelper.GetAllPermissions()
                            .GroupBy(p => p.Group)
                            .Select(g => new
                            {
                                group = g.Key,
                                permissions = g.Select(p => p.Permission)
                            });
            return Ok(response);
        }

        [Authorize(Policy = PermissionConstants.Permission.View)]
        [HttpGet("{roleId}/get-by-role")]
        public async Task<IActionResult> GetByRole(string roleId)
        {
            var response = await _sender.Send(new GetByRoleCommand(roleId));
            return Ok(response);

        }

    }
}

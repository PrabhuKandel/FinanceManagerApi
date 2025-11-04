using FinanceManager.Infrastructure.Authorization.Permissions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Api.Controllers
{
    [Route("api/permissions")]
    [ApiController]
    public class PermissionController() : ControllerBase
    {
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

    }
}

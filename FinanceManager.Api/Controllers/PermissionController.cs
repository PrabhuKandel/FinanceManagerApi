using FinanceManager.Application.Features.Permissions.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Api.Controllers
{
    [Route("api/permissions")]
    [ApiController]
    public class PermissionController(ISender sender) : ControllerBase
    {
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var response = await sender.Send(new GetAllPermissionsQuery());
            return Ok(response);
        }

    }
}

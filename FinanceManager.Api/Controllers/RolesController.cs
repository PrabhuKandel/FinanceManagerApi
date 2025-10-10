using Microsoft.AspNetCore.Mvc;
using FinanceManager.Application.Features.Roles.Queries;
using MediatR;
namespace FinanceManager.Api.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController(ISender sender) : ControllerBase
    {
        [HttpGet("get-all")]
        public async  Task<IActionResult> GetAll()
        {
            var response= await sender.Send(new GetAllRolesQuery());
            return Ok(response);
        }
    }
}

using FinanceManager.Application.Features.ApplicationUsers.Commands.UpdateApplicationUser;
using FinanceManager.Application.Features.ApplicationUsers.Queries.GetAllApplicationUsers;
using FinanceManager.Application.Features.ApplicationUsers.Queries.GetApplicationUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController(ISender sender) : ControllerBase
    {
 

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await sender.Send(new GetAllApplicationUsersQuery());

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await sender.Send(new GetApplicationUserByIdQuery(id));
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update( UpdateApplicationUserCommand command)
        {
            var response = await sender.Send(command);
            return Ok(response);
        }

    }
}

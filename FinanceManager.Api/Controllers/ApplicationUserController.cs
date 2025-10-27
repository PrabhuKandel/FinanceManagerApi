using FinanceManager.Application.Features.ApplicationUsers.Commands.UpdateApplicationUser;
using FinanceManager.Application.Features.ApplicationUsers.Queries.GetAllApplicationUsers;
using FinanceManager.Application.Features.ApplicationUsers.Queries.GetApplicationUserById;
using FinanceManager.Infrastructure.Authorization.Permissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApplicationUserController(ISender sender) : ControllerBase
    {

        [Authorize(Policy = PermissionConstants.ApplicationUserPernissions.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await sender.Send(new GetAllApplicationUsersQuery());

            return Ok(response);
        }

        [Authorize(Policy = PermissionConstants.ApplicationUserPernissions.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await sender.Send(new GetApplicationUserByIdQuery(id));
            return Ok(response);
        }

        [Authorize(Policy = PermissionConstants.ApplicationUserPernissions.Update)]
        [HttpPut]
        public async Task<IActionResult> Update( UpdateApplicationUserCommand command)
        {
            var response = await sender.Send(command);
            return Ok(response);
        }

    }
}

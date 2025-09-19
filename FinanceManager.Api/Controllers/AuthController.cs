    using FinanceManager.Application.Dtos.ApplicationUser;
using FinanceManager.Application.Features.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthController(IMediator _mediator )
        {
            mediator = _mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(ApplicationUserRegisterDto registerUser)
        {
            var response  =  await mediator.Send(new ApplicationUserRegisterCommand(registerUser));
                return Ok(response);
           
          

        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(ApplicationUserLoginDto loginUser)
        {
            var response = await mediator.Send(new ApplicationUserLoginCommand(loginUser));
            return Ok(response);

        }

        [Authorize]
        [HttpPatch("change-password")]
        public async Task<IActionResult> ChangePassword( ChangePasswordCommand command)
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("refresh-token")]

        public async Task<IActionResult> RefreshToken(string refreshToken)
        {

            var response  = await mediator.Send(new RefreshTokenCommand(refreshToken));
            return Ok(response);
        }

       
    }
}

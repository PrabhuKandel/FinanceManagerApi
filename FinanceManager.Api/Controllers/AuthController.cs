using FinanceManager.Application.Features.Auth.Commands.ApplicationUserLogin;
using FinanceManager.Application.Features.Auth.Commands.ApplicationUserRegister;
using FinanceManager.Application.Features.Auth.Commands.ChangePassword;
using FinanceManager.Application.Features.Auth.Commands.GeneratePasswordResetToken;
using FinanceManager.Application.Features.Auth.Commands.RefreshTokens;
using FinanceManager.Application.Features.Auth.Commands.ResetPassword;
using FinanceManager.Application.Features.Auth.Commands.RevokeToken;
using FinanceManager.Infrastructure.Authorization.Permissions;
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

        [Authorize(Policy = PermissionConstants.Auth.RegisterUser)]
        [HttpPost("register")]
        public async Task<IActionResult> Register(ApplicationUserRegisterCommand registerUser)
        {
            var response  =  await mediator.Send(registerUser);
                return Ok(response);
           
          

        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(ApplicationUserLoginCommand loginUser)
        {
            var response = await mediator.Send(loginUser);
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

        public async Task<IActionResult> RefreshToken(RefreshTokenCommand refreshToken)
        {

            var response  = await mediator.Send(refreshToken);
            return Ok(response);
        }


        [Authorize(Policy = PermissionConstants.Auth.RevokeToken)]
        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken(RevokeTokenCommand command)
        {
            var response = await mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("generate-password-reset-token")]
        public async Task<IActionResult> GeneratePasswordResetToken(GeneratePasswordResetToken command)
        {
            
            var response = await mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword( ResetPasswordCommand command)
        {
            var response = await mediator.Send(command);
            return Ok(response);

        }


    }
}

using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.ApplicationUser;
using FinanceManager.Application.Features.Auth.Commands;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class AuthController(IMediator _mediator ,IAuthService _authService) : ControllerBase
    {
    

        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(ApplicationUserRegisterDto registerUser)
        {
            var response  =  await _mediator.Send(new ApplicationUserRegisterCommand(registerUser));
                return Ok(response);
           
          

        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(ApplicationUserLoginDto loginUser)
        {
            var response = await _mediator.Send(new ApplicationUserLoginCommand(loginUser));
            return Ok(response);

        }

        [HttpPost("refresh-token")]

        public async Task<IActionResult> RefreshToken(string refreshToken)
        {

            var response = await _authService.RefreshTokenAsync(refreshToken);
            return Ok(response);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);  // should use new Claim(JwtRegisteredClaimNames.Sub, user.Id),  as claim while sending token

            var userId = User?.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new OperationResult<string> { Data = null, Message = "User not found" });
            var response = await _authService.LogoutAsync(userId);
            return Ok(response);
        }

    }
}

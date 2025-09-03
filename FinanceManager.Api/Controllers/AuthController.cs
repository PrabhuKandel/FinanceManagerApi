using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.ApplicationUser;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IAuthService _authService;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration config, IAuthService authService)
        {
            _userManager = userManager;
            _config = config;
            _authService = authService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(ApplicationUserRegisterDto registerUser)
        {
            var response = await _authService.RegisterAsync(registerUser);
            
                return Ok(response);
           
          

        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(ApplicationUserLoginDto loginUser)
        {


            var response = await _authService.LoginAsync(loginUser);

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
                return BadRequest(new ServiceResponse<string> { Data = null, Message = "User not found" });

            var response = await _authService.LogoutAsync(userId);

            return Ok(response);
        }

    }
}

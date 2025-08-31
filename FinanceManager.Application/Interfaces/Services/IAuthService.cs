using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.ApplicationUser;

namespace FinanceManager.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> RegisterAsync(ApplicationUserRegisterDto registerUser);
        Task<ServiceResponse<ApplicationUserLoginResponseDto>> LoginAsync(ApplicationUserLoginDto loginUser);
        Task<ServiceResponse<TokenResponseDto>> RefreshTokenAsync(string refreshToken);
        Task<ServiceResponse<string>> LogoutAsync(string userId);
        
        }
}

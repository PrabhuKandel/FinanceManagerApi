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
        Task<OperationResult<string>> RegisterAsync(ApplicationUserRegisterDto registerUser);
        Task<OperationResult<ApplicationUserLoginResponseDto>> LoginAsync(ApplicationUserLoginDto loginUser);
        Task<OperationResult<TokenResponseDto>> RefreshTokenAsync(string refreshToken);
        Task<OperationResult<string>> LogoutAsync(string userId);
        
        }
}

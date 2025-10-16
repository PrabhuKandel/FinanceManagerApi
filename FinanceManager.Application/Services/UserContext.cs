using System.Security.Claims;
using FinanceManager.Application.Common;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace FinanceManager.Application.Services
{
    public class UserContext:IUserContext
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId =>
            _httpContextAccessor.HttpContext?.User?.FindFirst("userId")?.Value
            ?? throw new AuthenticationException("User is not authenticated");

        public string Role =>
            _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value
            ?? throw new AuthenticationException("Role claim missing");

        public bool IsAdmin() => Role == RoleConstants.Admin;
    }
 
}

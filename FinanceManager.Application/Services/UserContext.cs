using System.Security.Claims;
using FinanceManager.Application.Common;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace FinanceManager.Application.Services
{
    public class UserContext:IUserContext
    {
        
        public string UserId { get; private set; }
        public string Role { get; private set; }
     
        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User
                  ?? throw new UnauthorizedAccessException();

            UserId = user.FindFirst("userId")?.Value
            ?? throw new AuthorizationException();

             Role = user.FindFirst(ClaimTypes.Role)?.Value 
               ?? throw new UnauthorizedAccessException();

           
        }
        public bool IsAdmin() => Role == RoleConstants.Admin;
    }
 
}

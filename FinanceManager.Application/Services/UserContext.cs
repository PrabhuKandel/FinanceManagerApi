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
            var user = httpContextAccessor.HttpContext?.User;
            if (user == null || !user.Identity!.IsAuthenticated)
                throw new AuthenticationException("User is not authenticated");


            UserId = user.FindFirst("userId")?.Value
            ?? throw new AuthenticationException("UserId claim missing");

            Role = user.FindFirst(ClaimTypes.Role)?.Value 
               ?? throw new AuthenticationException("Role claim missing");


        }
        public bool IsAdmin() => Role == RoleConstants.Admin;
    }
 
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace FinanceManager.Application.Services
{
    public class UserContext:IUserContext
    {
        
        public string UserId { get; private set; }
        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirst("userId")?.Value
            ?? throw new UnauthorizedAccessException();
        }
    }
 
}

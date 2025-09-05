using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FinanceManager.Application.Exceptions
{
    public class AuthenticationException : ApiException
    {
        public AuthenticationException(string message = "Invalid credentials") : base(message, StatusCodes.Status401Unauthorized) { }
    }
}

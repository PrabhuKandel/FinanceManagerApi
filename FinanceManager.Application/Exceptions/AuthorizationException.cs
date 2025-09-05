using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FinanceManager.Application.Exceptions
{
    public class AuthorizationException : ApiException
    {
        public AuthorizationException(string message = "Access denied") : base(message, StatusCodes.Status403Forbidden) { }
    }
}

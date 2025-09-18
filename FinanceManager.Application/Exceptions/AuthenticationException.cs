using Microsoft.AspNetCore.Http;

namespace FinanceManager.Application.Exceptions
{
    public class AuthenticationException : ApiException
    {
        public AuthenticationException(string message = "Invalid credentials") : base(message, StatusCodes.Status401Unauthorized) { }
    }
}

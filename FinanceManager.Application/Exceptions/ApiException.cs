using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FinanceManager.Application.Exceptions
{
    public abstract class ApiException : Exception
    {
        public int StatusCode { get; }
        public IDictionary<string, string>? Errors { get; }

        protected ApiException(string message, int statusCode= StatusCodes.Status500InternalServerError, IDictionary<string, string>? errors = null)
            : base(message)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
    }
}

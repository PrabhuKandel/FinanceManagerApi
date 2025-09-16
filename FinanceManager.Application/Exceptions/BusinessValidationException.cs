

using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace FinanceManager.Application.Exceptions
{
    public class BusinessValidationException: ApiException
    {

        public BusinessValidationException(IDictionary<string, string[]> errors)
                  : base("Validation failed", 400, errors) { }
        
            
        public BusinessValidationException(string errorMessage = "Validation failed")
        : base(errorMessage, StatusCodes.Status400BadRequest)
        {
           
        }
    }
}

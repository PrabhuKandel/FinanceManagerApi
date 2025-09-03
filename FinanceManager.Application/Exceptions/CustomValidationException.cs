using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FinanceManager.Application.Exceptions
{
    public class CustomValidationException: Exception
    {
        public IDictionary<string, string>? Errors { get; }

        public CustomValidationException(IDictionary<string, string> errors)
                  : base("Validation failed")
        {
            Errors = errors;
        }
        public CustomValidationException(string errorMessage)
        : base("Validation failed")
        {
            Errors = new Dictionary<string, string> { { "Error", errorMessage } };
        }
    }
}

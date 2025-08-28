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
        public IEnumerable<string> Errors { get; }

        public CustomValidationException(IEnumerable<string> errors)
                  : base("Validation failed")
        {
            Errors = errors;
        }
    }
}

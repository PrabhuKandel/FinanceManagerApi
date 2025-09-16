using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Application.Exceptions
{
    public class ExceptionResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public IDictionary<string, string[]>? Errors { get; set; }
    }
}

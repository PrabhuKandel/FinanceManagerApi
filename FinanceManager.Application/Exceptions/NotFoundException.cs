using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FinanceManager.Application.Exceptions
{
    public class NotFoundException:ApiException
    {
       
        public NotFoundException(string message) : base(message, StatusCodes.Status404NotFound) { }
    }
}

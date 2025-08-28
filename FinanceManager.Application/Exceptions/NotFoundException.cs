using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Application.Exceptions
{
    public class NotFoundException:Exception
    {
        //status code 404
        public NotFoundException(string message) : base(message) { }
    }
}

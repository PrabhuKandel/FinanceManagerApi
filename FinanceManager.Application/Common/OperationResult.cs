using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Application.Common
{
    public class OperationResult<T>
    {
      
        public string ?Message { get; set; }
        public T? Data { get; set; }

        
    }
}

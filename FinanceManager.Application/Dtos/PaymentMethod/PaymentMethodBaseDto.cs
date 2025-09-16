using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Application.Dtos.PaymentMethod
{
    public class PaymentMethodBaseDto
    {

        public required string Name { get; set; }


        public string? Description { get; set; }


        public bool IsActive { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Domain.Entities;

namespace FinanceManager.Application.Dtos.PaymentMethod
{
    public class PaymentMethodCreateDto
    {
       
        public string Name { get; set; }

        
        public string? Description { get; set; }

      
        public bool ?IsActive { get; set; }
    }
}

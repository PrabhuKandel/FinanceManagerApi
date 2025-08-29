using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FinanceManager.Domain.Models;

namespace FinanceManager.Application.Dtos.PaymentMethod
{
    public class PaymentMethodResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public bool IsActive{ get; set; }
    }
}

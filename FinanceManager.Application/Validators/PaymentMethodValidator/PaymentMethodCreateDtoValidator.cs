using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Infrastructure.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Validators.PaymentMethodValidator
{
    public class PaymentMethodCreateDtoValidator : PaymentMethodBaseDtoValidator<PaymentMethodCreateDto>
    {
        
    
        public PaymentMethodCreateDtoValidator(ApplicationDbContext _context)  : base(_context)
        {


        }
}
}

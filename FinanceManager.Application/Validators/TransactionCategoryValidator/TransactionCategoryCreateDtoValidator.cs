using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Infrastructure.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Validators.TransactionCategoryValidator
{
    public class TransactionCategoryCreateDtoValidator:TransactionCategoryBaseDtoValidator<TransactionCategoryCreateDto>
    {
        public TransactionCategoryCreateDtoValidator(ApplicationDbContext _context) : base(_context)
        {
           

           
        }
    }
}

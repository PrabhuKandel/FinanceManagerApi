    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Infrastructure.Data;
using FluentValidation;

namespace FinanceManager.Application.Validators.TransactionRecordValidator
{
    public class TransactionRecordUpdateDtoValidator : TransactionRecordBaseDtoValidator<TransactionRecordUpdateDto>
    {
        public TransactionRecordUpdateDtoValidator(ApplicationDbContext _context):base(_context)
        {
      
        }
    }
}

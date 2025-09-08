using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Infrastructure.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Validators.TransactionRecordValidator
{
    public class TransactionRecordCreateDtoValidator : TransactionRecordBaseDtoValidator<TransactionRecordCreateDto>
    {
        public TransactionRecordCreateDtoValidator(ApplicationDbContext _context) : base(_context)
        {

        }
    }
}

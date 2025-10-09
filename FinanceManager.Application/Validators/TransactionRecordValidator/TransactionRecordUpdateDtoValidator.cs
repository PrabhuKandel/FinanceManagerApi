using System.Threading;
using FinanceManager.Application.Dtos.TransactionPayment;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Validators.TransactionPaymentValidator;
using FluentValidation;
using Microsoft.EntityFrameworkCore;


namespace FinanceManager.Application.Validators.TransactionRecordValidator
{
    public class TransactionRecordUpdateDtoValidator : TransactionRecordBaseDtoValidator<TransactionRecordUpdateDto>
    {
        public TransactionRecordUpdateDtoValidator(IApplicationDbContext _context) : base(_context)
        {

  

        }
    }
}

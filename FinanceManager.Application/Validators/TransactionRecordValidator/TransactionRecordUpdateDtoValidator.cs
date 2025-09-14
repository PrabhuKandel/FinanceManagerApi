using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Application.Interfaces;


namespace FinanceManager.Application.Validators.TransactionRecordValidator
{
    public class TransactionRecordUpdateDtoValidator : TransactionRecordBaseDtoValidator<TransactionRecordUpdateDto>
    {
        public TransactionRecordUpdateDtoValidator(IApplicationDbContext _context):base(_context)
        {
      
        }
    }
}

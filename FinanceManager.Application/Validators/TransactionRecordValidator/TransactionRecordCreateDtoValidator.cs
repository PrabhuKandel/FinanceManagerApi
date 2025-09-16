using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Application.Interfaces;


namespace FinanceManager.Application.Validators.TransactionRecordValidator
{
    public class TransactionRecordCreateDtoValidator : TransactionRecordBaseDtoValidator<TransactionRecordCreateDto>
    {
        public TransactionRecordCreateDtoValidator(IApplicationDbContext _context) : base(_context)
        {

        }
    }
}

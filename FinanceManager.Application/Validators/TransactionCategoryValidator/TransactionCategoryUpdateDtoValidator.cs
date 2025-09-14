using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Interfaces;

namespace FinanceManager.Application.Validators.TransactionCategoryValidator
{
    public class TransactionCategoryUpdateDtoValidator:TransactionCategoryBaseDtoValidator<TransactionCategoryUpdateDto>
    {
        public TransactionCategoryUpdateDtoValidator(IApplicationDbContext _context): base (_context)
        {

          
        }
    }
}

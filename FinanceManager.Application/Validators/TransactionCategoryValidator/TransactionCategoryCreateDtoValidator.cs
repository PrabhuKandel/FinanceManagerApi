using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Interfaces;


namespace FinanceManager.Application.Validators.TransactionCategoryValidator
{
    public class TransactionCategoryCreateDtoValidator:TransactionCategoryBaseDtoValidator<TransactionCategoryCreateDto>
    {
        public TransactionCategoryCreateDtoValidator(IApplicationDbContext _context) : base(_context)
        {
           

           
        }
    }
}

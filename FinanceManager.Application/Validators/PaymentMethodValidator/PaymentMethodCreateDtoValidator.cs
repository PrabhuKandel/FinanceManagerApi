using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Interfaces;

namespace FinanceManager.Application.Validators.PaymentMethodValidator
{
    public class PaymentMethodCreateDtoValidator : PaymentMethodBaseDtoValidator<PaymentMethodCreateDto>
    {
        
    
        public PaymentMethodCreateDtoValidator(IApplicationDbContext _context)  : base(_context)
        {


        }
}
}

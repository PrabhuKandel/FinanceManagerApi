using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Infrastructure.Data;


namespace FinanceManager.Application.Validators.PaymentMethodValidator
{
    public class PaymentMethodCreateDtoValidator : PaymentMethodBaseDtoValidator<PaymentMethodCreateDto>
    {
        
    
        public PaymentMethodCreateDtoValidator(ApplicationDbContext _context)  : base(_context)
        {


        }
}
}

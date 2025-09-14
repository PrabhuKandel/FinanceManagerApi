using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Interfaces;

namespace FinanceManager.Application.Validators.PaymentMethodValidator
{
    public class PaymentMethodUpdateDtoValidator : PaymentMethodBaseDtoValidator<PaymentMethodUpdateDto>
    {
        public PaymentMethodUpdateDtoValidator(IApplicationDbContext _context):base(_context)
        {


        }
    }
}

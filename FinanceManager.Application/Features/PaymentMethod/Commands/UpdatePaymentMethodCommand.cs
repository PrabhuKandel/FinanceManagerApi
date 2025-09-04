using FinanceManager.Application.Dtos.PaymentMethod;
using MediatR;

namespace FinanceManager.Application.Features.PaymentMethod.Commands
{
    public  record UpdatePaymentMethodCommand(Guid Id, PaymentMethodUpdateDto paymentMethod):IRequest<PaymentMethodResponseDto>
    {

    }
}

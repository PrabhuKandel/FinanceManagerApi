using FinanceManager.Application.Dtos.PaymentMethod;
using MediatR;

namespace FinanceManager.Application.Features.PaymentMethods.Commands
{
    public  record UpdatePaymentMethodCommand(Guid Id, PaymentMethodUpdateDto paymentMethod):IRequest<PaymentMethodResponseDto>
    {

    }
}

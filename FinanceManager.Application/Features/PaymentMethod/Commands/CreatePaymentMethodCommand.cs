using FinanceManager.Application.Dtos.PaymentMethod;
using MediatR;

namespace FinanceManager.Application.Features.PaymentMethod.Commands
{
    public  record CreatePaymentMethodCommand(PaymentMethodCreateDto paymentMethod):IRequest<PaymentMethodResponseDto>
    {

    }
}

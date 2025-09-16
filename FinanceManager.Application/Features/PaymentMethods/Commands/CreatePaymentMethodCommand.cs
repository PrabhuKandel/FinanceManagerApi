using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using MediatR;

namespace FinanceManager.Application.Features.PaymentMethods.Commands
{
    public record CreatePaymentMethodCommand(PaymentMethodCreateDto PaymentMethod) : IRequest<OperationResult<PaymentMethodResponseDto>>
    {

    }


}

using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using MediatR;

namespace FinanceManager.Application.Features.PaymentMethods.Commands
{
    public  record UpdatePaymentMethodCommand(Guid Id, PaymentMethodUpdateDto PaymentMethod):IRequest<OperationResult<PaymentMethodResponseDto>>
    {

    }
}

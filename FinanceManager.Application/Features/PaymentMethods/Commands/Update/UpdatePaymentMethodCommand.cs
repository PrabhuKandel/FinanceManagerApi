using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using MediatR;

namespace FinanceManager.Application.Features.PaymentMethods.Commands.Update
{
    public  record UpdatePaymentMethodCommand
        (
           Guid Id,
           string Name,
           string? Description,
           bool IsActive
        ) :IRequest<OperationResult<PaymentMethodResponseDto>>
    {

    }
}

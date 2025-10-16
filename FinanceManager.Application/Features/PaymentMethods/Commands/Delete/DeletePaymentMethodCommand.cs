using FinanceManager.Application.Common;
using MediatR;

namespace FinanceManager.Application.Features.PaymentMethods.Commands.Delete
{
    public  record DeletePaymentMethodCommand(Guid Id):IRequest<OperationResult<string>>
    {
    }
}

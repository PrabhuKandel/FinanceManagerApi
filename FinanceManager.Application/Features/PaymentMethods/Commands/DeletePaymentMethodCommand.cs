using FinanceManager.Application.Common;
using MediatR;

namespace FinanceManager.Application.Features.PaymentMethods.Commands
{
    public  record DeletePaymentMethodCommand(Guid Id):IRequest<OperationResult<string>>
    {
    }
}

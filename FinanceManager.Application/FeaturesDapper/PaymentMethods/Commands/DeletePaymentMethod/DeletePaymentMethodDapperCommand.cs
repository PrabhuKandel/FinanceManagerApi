using FinanceManager.Application.Common;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.PaymentMethods.Commands.DeletePaymentMethod
{
    public record DeletePaymentMethodDapperCommand(Guid Id) : IRequest<OperationResult<string>>
    {
    }
}

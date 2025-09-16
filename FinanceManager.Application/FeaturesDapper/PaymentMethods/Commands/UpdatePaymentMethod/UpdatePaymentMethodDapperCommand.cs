using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.PaymentMethods.Commands.UpdatePaymentMethod
{
    public record UpdatePaymentMethodDapperCommand(Guid Id, string Name, string? Description, bool IsActive) : IRequest<OperationResult<PaymentMethodResponseDto>>
    {
    }
}

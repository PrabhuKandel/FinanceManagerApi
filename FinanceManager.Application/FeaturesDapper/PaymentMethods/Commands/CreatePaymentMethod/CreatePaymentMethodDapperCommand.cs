using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.PaymentMethods.Commands.CreatePaymentMethod
{
    public record CreatePaymentMethodDapperCommand(string Name, string? Description, bool IsActive): IRequest<OperationResult<PaymentMethodResponseDto>>
    {
    }
}

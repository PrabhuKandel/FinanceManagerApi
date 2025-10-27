using FinanceManager.Application.Common;
using FinanceManager.Application.Features.PaymentMethods.Dtos;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.PaymentMethods.Commands.CreatePaymentMethod
{
    public record CreatePaymentMethodDapperCommand(string Name, string? Description, bool IsActive): IRequest<OperationResult<PaymentMethodResponseDto>>
    {
    }
}

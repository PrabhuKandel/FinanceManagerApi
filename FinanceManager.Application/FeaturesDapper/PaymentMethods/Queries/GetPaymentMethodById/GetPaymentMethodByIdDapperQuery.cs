using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.PaymentMethods.Queries.GetPaymentMethodById
{
    public record GetPaymentMethodByIdDapperQuery (Guid Id):IRequest<OperationResult<PaymentMethodResponseDto>>
    {
    }
}

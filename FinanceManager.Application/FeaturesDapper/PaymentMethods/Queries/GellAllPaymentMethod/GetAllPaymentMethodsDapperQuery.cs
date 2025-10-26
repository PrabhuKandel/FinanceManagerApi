using FinanceManager.Application.Common;
using FinanceManager.Application.Features.PaymentMethods.Dtos;
using MediatR;


namespace FinanceManager.Application.FeaturesDapper.PaymentMethods.Queries.GellAllPaymentMethod
{
    public record GetAllPaymentMethodsDapperQuery() : IRequest<OperationResult<IEnumerable<PaymentMethodResponseDto>>>
    {

    }

}

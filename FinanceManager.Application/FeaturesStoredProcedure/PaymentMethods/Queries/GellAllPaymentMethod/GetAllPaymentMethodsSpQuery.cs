using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using MediatR;


namespace FinanceManager.Application.FeaturesStoredProcedure.PaymentMethods.Queries.GellAllPaymentMethod
{
    public record GetAllPaymentMethodsSpQuery() : IRequest<OperationResult<IEnumerable<PaymentMethodResponseDto>>>
    {

    }

}

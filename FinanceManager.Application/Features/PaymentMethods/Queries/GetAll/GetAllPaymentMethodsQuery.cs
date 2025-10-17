using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using MediatR;

namespace FinanceManager.Application.Features.PaymentMethods.Queries.GetAll
{
    public  class GetAllPaymentMethodsQuery: IRequest<OperationResult<IEnumerable<PaymentMethodResponseDto>>>
    {
    }
}

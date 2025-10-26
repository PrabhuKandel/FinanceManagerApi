using FinanceManager.Application.Common;
using FinanceManager.Application.Features.PaymentMethods.Dtos;
using MediatR;


namespace FinanceManager.Application.FeaturesStoredProcedure.PaymentMethods.Queries.GetPaymentMethodById
{
    public record GetPaymentMethodByIdSpQuery(Guid Id) : IRequest<OperationResult<PaymentMethodResponseDto>>
    {

    }


}
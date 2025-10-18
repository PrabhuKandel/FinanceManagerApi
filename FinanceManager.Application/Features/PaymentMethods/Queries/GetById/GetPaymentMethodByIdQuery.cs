using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using MediatR;

namespace FinanceManager.Application.Features.PaymentMethods.Queries.GetById
{

    // previously used class instead of record and throws error check it later......
    public record GetPaymentMethodByIdQuery(Guid Id):IRequest<OperationResult<PaymentMethodResponseDto>>
    {
    }
}

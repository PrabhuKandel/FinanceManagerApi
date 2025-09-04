using FinanceManager.Application.Dtos.PaymentMethod;
using MediatR;

namespace FinanceManager.Application.Features.PaymentMethod.Queries
{

    // previously used class instead of record and throws error check it later......
    public record GetPaymentMethodByIdQuery(Guid Id):IRequest<PaymentMethodResponseDto>
    {
    }
}

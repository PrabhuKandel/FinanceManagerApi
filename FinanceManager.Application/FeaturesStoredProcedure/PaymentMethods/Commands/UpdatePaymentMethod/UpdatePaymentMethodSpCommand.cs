using FinanceManager.Application.Common;
using FinanceManager.Application.Features.PaymentMethods.Dtos;
using MediatR;


namespace FinanceManager.Application.FeaturesStoredProcedure.PaymentMethods.Commands.UpdatePaymentMethod
{
    public record  UpdatePaymentMethodSpCommand (Guid Id, string Name, string? Description, bool IsActive) : IRequest<OperationResult<PaymentMethodResponseDto>>
    {
    }

}

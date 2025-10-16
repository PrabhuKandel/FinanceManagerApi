using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using MediatR;

namespace FinanceManager.Application.Features.PaymentMethods.Commands.Create
{
    public record CreatePaymentMethodCommand(

      string Name,
      string?Description,
      bool IsActive
        
        ) : IRequest<OperationResult<PaymentMethodResponseDto>>
    {
 
    }


}

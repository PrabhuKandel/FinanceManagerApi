using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using MediatR;


namespace FinanceManager.Application.FeaturesStoredProcedure.PaymentMethods.Commands.UpdatePaymentMethod
{
    public record  UpdatePaymentMethodSpCommand (Guid Id, string Name, string? Description, bool IsActive) : IRequest<OperationResult<PaymentMethodResponseDto>>
    {
    }

}

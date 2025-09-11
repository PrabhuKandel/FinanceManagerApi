using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Mapping;
using FinanceManager.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.FeaturesStoredProcedure.PaymentMethods.Commands.CreatePaymentMethod
{
    public  record CreatePaymentMethodSpCommand(string Name, string? Description , bool IsActive): IRequest<OperationResult<PaymentMethodResponseDto>>
    {
    }

}

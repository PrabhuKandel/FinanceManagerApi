using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Common;
using FinanceManager.Application.Exceptions;
using FinanceManager.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.FeaturesStoredProcedure.PaymentMethods.Commands.DeletePaymentMethod
{
    public  record  DeletePaymentMethodSpCommand(Guid Id): IRequest<OperationResult<string>>
    {
    }


}

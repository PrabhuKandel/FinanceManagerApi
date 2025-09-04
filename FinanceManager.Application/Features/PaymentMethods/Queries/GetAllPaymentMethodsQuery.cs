using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Dtos.PaymentMethod;
using MediatR;

namespace FinanceManager.Application.Features.PaymentMethods.Queries
{
    public  class GetAllPaymentMethodsQuery:IRequest<IEnumerable<PaymentMethodResponseDto>>
    {
    }
}

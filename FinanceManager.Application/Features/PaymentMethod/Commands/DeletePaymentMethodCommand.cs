using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FinanceManager.Application.Features.PaymentMethod.Commands
{
    public  record DeletePaymentMethodCommand(Guid Id):IRequest<string>
    {
    }
}

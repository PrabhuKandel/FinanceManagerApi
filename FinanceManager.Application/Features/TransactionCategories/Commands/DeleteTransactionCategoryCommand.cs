using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Common;
using MediatR;

namespace FinanceManager.Application.Features.TransactionCategories.Commands
{
    public  record DeleteTransactionCategoryCommand(Guid Id):IRequest<OperationResult<string>>
    {
    }
}

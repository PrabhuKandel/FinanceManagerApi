using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Common;
using MediatR;

namespace FinanceManager.Application.FeaturesStoredProcedure.TransactionCategory.Commands.DeleteTransactionCategory
{
    public  record DeleteTransactionCategorySpCommand(Guid Id) : IRequest<OperationResult<string>>
    {
    }
}

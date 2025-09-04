using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionCategory;
using MediatR;

namespace FinanceManager.Application.Features.TransactionCategories.Queries
{
    public  class GetAllTransactionCategoriesQuery:IRequest<OperationResult<IEnumerable<TransactionCategoryResponseDto>>>
    {
    }
}

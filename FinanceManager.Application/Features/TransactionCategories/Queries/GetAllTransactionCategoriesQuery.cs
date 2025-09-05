using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionCategory;
using MediatR;

namespace FinanceManager.Application.Features.TransactionCategories.Queries
{
    public  class GetAllTransactionCategoriesQuery:IRequest<OperationResult<IEnumerable<TransactionCategoryResponseDto>>>
    {
    }
}

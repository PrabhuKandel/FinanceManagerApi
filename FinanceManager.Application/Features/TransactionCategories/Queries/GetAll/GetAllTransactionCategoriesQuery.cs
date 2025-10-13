using FinanceManager.Application.Common;
using FinanceManager.Application.Features.TransactionCategories.Dtos;
using MediatR;

namespace FinanceManager.Application.Features.TransactionCategories.Queries.GetAll
{
    public  record GetAllTransactionCategoriesQuery:IRequest<OperationResult<IEnumerable<TransactionCategoryResponseDto>>>
    {
    }
}

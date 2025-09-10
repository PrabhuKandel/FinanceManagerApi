using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionCategory;
using MediatR;

namespace FinanceManager.Application.Features.TransactionCategories.Commands
{
    public  record UpdateTransactionCategoryCommand(Guid Id, TransactionCategoryUpdateDto TransactionCategory):IRequest<OperationResult<TransactionCategoryResponseDto>>
    {

    }
}

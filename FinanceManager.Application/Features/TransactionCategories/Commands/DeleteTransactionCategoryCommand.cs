using FinanceManager.Application.Common;
using MediatR;

namespace FinanceManager.Application.Features.TransactionCategories.Commands
{
    public  record DeleteTransactionCategoryCommand(Guid Id):IRequest<OperationResult<string>>
    {
    }
}

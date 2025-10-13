using FinanceManager.Application.Common;
using MediatR;

namespace FinanceManager.Application.Features.TransactionCategories.Commands.Delete
{
    public  record DeleteTransactionCategoryCommand(Guid Id):IRequest<OperationResult<string>>
    {
    }
}

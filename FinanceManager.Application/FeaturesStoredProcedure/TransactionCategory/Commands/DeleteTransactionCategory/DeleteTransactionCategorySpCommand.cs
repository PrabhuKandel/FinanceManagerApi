using FinanceManager.Application.Common;
using MediatR;

namespace FinanceManager.Application.FeaturesStoredProcedure.TransactionCategory.Commands.DeleteTransactionCategory
{
    public  record DeleteTransactionCategorySpCommand(Guid Id) : IRequest<OperationResult<string>>
    {
    }
}

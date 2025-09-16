using FinanceManager.Application.Common;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.TransactionRecords.Commands.DeleteTransactionRecord
{
    public record DeleteTransactionRecordDapperCommand(Guid Id) : IRequest<OperationResult<string>>
    {
    }
}

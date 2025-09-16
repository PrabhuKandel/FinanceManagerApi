

using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.TransactionRecords.Queries.GetAllTransactionRecord
{
    public record GetAllTransactionRecordsDapperQuery():IRequest<OperationResult<IEnumerable<TransactionRecordResponseDto>>>
    {

    }
}

using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.TransactionRecords.Queries.GetTransactionRecordById
{
    public record GetTransactionRecordByIdDapperQuery(Guid Id): IRequest<OperationResult<TransactionRecordResponseDto>>
    {
    }
}

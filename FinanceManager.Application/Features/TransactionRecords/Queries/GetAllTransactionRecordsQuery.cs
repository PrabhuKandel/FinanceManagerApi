using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Queries
{
    public record GetAllTransactionRecordsQuery(int PageNumber = 1, int PageSize = 10) :IRequest<PaginatedOperationResult<IEnumerable<TransactionRecordResponseDto>>>
    {
    }
}

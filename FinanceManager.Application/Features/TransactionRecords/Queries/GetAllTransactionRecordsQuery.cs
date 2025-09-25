using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Queries
{
    public class GetAllTransactionRecordsQuery :IRequest<PaginatedOperationResult<IEnumerable<TransactionRecordResponseDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}

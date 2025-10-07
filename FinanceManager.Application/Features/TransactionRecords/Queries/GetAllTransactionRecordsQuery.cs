using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Domain.Enums;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Queries
{
    public class GetAllTransactionRecordsQuery :IRequest<PaginatedOperationResult<IEnumerable<TransactionRecordResponseDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }

        public TransactionRecordApprovalStatus? ApprovalStatus { get; set; }

        public string? Search { get; set; }

        public string? SortBy { get; set; }     
        public bool SortDescending { get; set; }


    }
}

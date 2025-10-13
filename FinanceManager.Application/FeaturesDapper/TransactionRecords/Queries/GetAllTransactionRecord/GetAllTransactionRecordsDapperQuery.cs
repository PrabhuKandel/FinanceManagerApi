

using FinanceManager.Application.Common;
using FinanceManager.Application.Features.TransactionRecords.Dtos;
using FinanceManager.Domain.Enums;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.TransactionRecords.Queries.GetAllTransactionRecord
{
    public record GetAllTransactionRecordsDapperQuery(
        int PageNumber = 1,
        int PageSize = 10,
        bool SortDescending = false,
        DateTime? FromDate = null,
        DateTime? ToDate = null,
        string? CreatedBy = null,
        string? UpdatedBy = null,
        TransactionRecordApprovalStatus? ApprovalStatus=null ,
        string? Search = null,
        string? SortBy = null
    ) : IRequest<PaginatedOperationResult<IEnumerable<TransactionRecordResponseDto>>>;

}

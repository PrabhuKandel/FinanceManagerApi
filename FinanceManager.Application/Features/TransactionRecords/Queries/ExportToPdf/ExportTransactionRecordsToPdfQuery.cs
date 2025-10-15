

using FinanceManager.Domain.Enums;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Queries.ExportToPdf
{
    public record ExportTransactionRecordsToPdfQuery(
        int PageNumber = 1,
        int PageSize = 10,
        bool SortDescending = true,
        DateTime? FromDate = null,
        DateTime? ToDate = null,
        string? CreatedBy = null,
        string? UpdatedBy = null,
        TransactionRecordApprovalStatus? ApprovalStatus = null,
        string? Search = null,
        string? SortBy = null) : IRequest<byte[]>
       
    {
    }
}

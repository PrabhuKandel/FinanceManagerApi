using FinanceManager.Application.Common;
using FinanceManager.Application.FeaturesDapper.Reports.Dtos;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.Reports.Queries.TransactionRecordSummaryByTransactionCategory
{
    public record TransactionRecordSummaryByTransactionCategoryQuery
        (
           Guid? TransactionCategoryId
        , DateTime? FromDate
        , DateTime? ToDate
        ):IRequest<OperationResult<IEnumerable<TransactionRecordSummaryByCategoryDto>>>
    {
    }
}

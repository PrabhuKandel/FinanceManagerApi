

using FinanceManager.Application.Common;
using FinanceManager.Application.FeaturesDapper.Reports.Dtos;
using FinanceManager.Application.FeaturesDapper.Reports.Enums;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.Reports.Queries.TransactionRecordSummaryByCategoryType
{
    public record TransactionRecordSummaryByCategoryTypeQuery
        (
        DateTime? FromDate
        ,DateTime? ToDate,
        Period GroupBy = Period.Month
        ) :IRequest<OperationResult<IEnumerable<TransactionRecordSummaryByCategoryTypeDto>>>
    {
    }
}

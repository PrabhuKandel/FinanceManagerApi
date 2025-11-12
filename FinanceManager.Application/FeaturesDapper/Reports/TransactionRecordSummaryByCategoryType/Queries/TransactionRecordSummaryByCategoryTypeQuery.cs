using FinanceManager.Application.Common;
using FinanceManager.Application.FeaturesDapper.Reports.Dtos;
using FinanceManager.Domain.Enums;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.Reports.TransactionRecordSummaryByCategoryType.Queries
{
    public record TransactionRecordSummaryByCategoryTypeQuery
        (
        DateTime? FromDate
        ,DateTime? ToDate,
        PeriodType GroupBy = PeriodType.Monthly
        ) :IRequest<OperationResult<IEnumerable<TransactionRecordSummaryByCategoryTypeDto>>>
    {
    }
}

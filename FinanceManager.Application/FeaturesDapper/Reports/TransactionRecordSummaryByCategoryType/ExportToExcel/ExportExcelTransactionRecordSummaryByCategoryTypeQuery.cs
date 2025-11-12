using FinanceManager.Domain.Enums;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.Reports.TransactionRecordSummaryByCategoryType.ExportToExcel
{
    public record ExportExcelTransactionRecordSummaryByCategoryTypeQuery
        (
           DateTime? FromDate
        , DateTime? ToDate,
        PeriodType GroupBy = PeriodType.Monthly
        ) : IRequest<byte[]>
    {
    }
}

using MediatR;

namespace FinanceManager.Application.FeaturesDapper.Reports.TransactionRecordSummaryByTransactionCategory.ExportToExcel
{
    public record ExportExcelTransactionRecordSummaryByTransactionCategoryQuery
        (
           Guid? TransactionCategoryId
        , DateTime? FromDate
        , DateTime? ToDate
        ) : IRequest<byte[]>
    {
    }
}

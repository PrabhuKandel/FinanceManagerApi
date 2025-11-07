

using MediatR;

namespace FinanceManager.Application.FeaturesDapper.Reports.Queries.TransactionRecordSummaryByPaymentMethod.ExportToExcel
{
    public record ExportExcelTransactionRecordSummaryByPaymentMethodQuery
        (
        Guid? PaymentMethodId
        , DateTime? FromDate
        , DateTime? ToDate
        ):IRequest<byte[]>
    {
    }
}

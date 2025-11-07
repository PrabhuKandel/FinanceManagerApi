
using FinanceManager.Application.FeaturesDapper.Reports.Dtos;
using FinanceManager.Application.Interfaces.Services;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.Reports.Queries.TransactionRecordSummaryByPaymentMethod.ExportToExcel
{
    public class ExportExcelTransactionRecordSummaryByPaymentMethodHandler(
        ISender sender,
        IExcelBuilder<IEnumerable<TransactionRecordSummaryByPaymentMethodDto>> _excelBuilder,
        IExcelService _excelService

        ) : IRequestHandler<ExportExcelTransactionRecordSummaryByPaymentMethodQuery, byte[]>
    {
        public  async Task<byte[]> Handle(ExportExcelTransactionRecordSummaryByPaymentMethodQuery request, CancellationToken cancellationToken)
        {
           var reportData = new TransactionRecordSummaryByPaymentMethodQuery
                                    (
                                        request.PaymentMethodId,
                                        request.FromDate,
                                        request.ToDate
                                    );
            var reportExportData = await sender.Send(reportData, cancellationToken);

            var workbook = _excelBuilder.Build(reportExportData.Data);

            return _excelService.SaveWorkbook(workbook);


        }
    }
}

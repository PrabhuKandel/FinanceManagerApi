using FinanceManager.Application.FeaturesDapper.Reports.Dtos;
using FinanceManager.Application.Interfaces.Services;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.Reports.Queries.TransactionRecordSummaryByTransactionCategory.ExportToExcel
{
    public class ExportExcelTransactionRecordSummaryByTransactionCategoryHandler
        (
        ISender sender,
        IExcelBuilder<IEnumerable<TransactionRecordSummaryByCategoryDto>> _excelBuilder,
        IExcelService _excelService



        ) : IRequestHandler<ExportExcelTransactionRecordSummaryByTransactionCategoryQuery, byte[]>
    {
        public async Task<byte[]> Handle(ExportExcelTransactionRecordSummaryByTransactionCategoryQuery request, CancellationToken cancellationToken)
        {
            var reportData = new TransactionRecordSummaryByTransactionCategoryQuery
                                     (
                                         request.TransactionCategoryId,
                                         request.FromDate,
                                         request.ToDate
                                     );
            var reportExportData = await sender.Send(reportData, cancellationToken);

            var workbook = _excelBuilder.Build(reportExportData.Data);

            return _excelService.SaveWorkbook(workbook);
        }
    }
}

using FinanceManager.Application.FeaturesDapper.Reports.Dtos;
using FinanceManager.Application.FeaturesDapper.Reports.TransactionCategoryBudgetVsActualOutflow.Queries;
using FinanceManager.Application.Interfaces.Services;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.Reports.TransactionCategoryBudgetVsActualOutflow.ExportToExcel
{
    public class ExportExcelTransactionCategoryBudgetVsActualOutflowHandler(
        ISender sender,
        IExcelBuilder<IEnumerable<TransactionCategoryBudgetVsActualOutflowDto>> _excelBuilder,
        IExcelService _excelService


        ) : IRequestHandler<ExportExcelTransactionCategoryBudgetVsActualOutflowQuery, byte[]>
    {
        public async Task<byte[]> Handle(ExportExcelTransactionCategoryBudgetVsActualOutflowQuery request, CancellationToken cancellationToken)
        {
            var ReportData = new TransactionCategoryBudgetVsActualOutflowQuery
                                     (
                                         request.TransactionCategoryId,
                                         request.PeriodType,
                                         request.PeriodStart,
                                         request.PeriodEnd
                                     );

            var reportExportData = await sender.Send(ReportData, cancellationToken);

           var workbook =  _excelBuilder.Build(reportExportData.Data);

          return  _excelService.SaveWorkbook(workbook);


        }
    }
}

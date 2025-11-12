using FinanceManager.Application.FeaturesDapper.Reports.TransactionCategoryBudgetVsActualOutflow.Queries;
using FinanceManager.Application.Interfaces.Services;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.Reports.TransactionCategoryBudgetVsActualOutflow.ExportToPdf
{
    public class ExportTransactionCategoryBudgetVsActualOutflowHandler(ISender sender, ITemplateRenderer _templateRenderer, IPdfGenerator _pdfGenerator) : IRequestHandler<ExportTransactionCategoryBudgetVsActualOutflowQuery, byte[]>
    {
        public async Task<byte[]> Handle(ExportTransactionCategoryBudgetVsActualOutflowQuery request, CancellationToken cancellationToken)
        {
            var ReportData =  new TransactionCategoryBudgetVsActualOutflowQuery
            (
                request.TransactionCategoryId,
                request.PeriodType,
                request.PeriodStart,
                request.PeriodEnd
            );

            var reportExportData = await sender.Send(ReportData, cancellationToken);

            DateTime periodStart = DateTime.Parse(request.PeriodStart);
            DateTime periodEnd = DateTime.Parse(request.PeriodEnd);
            if (reportExportData.Data != null && reportExportData.Data.Any())
            {
                 periodStart = reportExportData.Data.First().PeriodStart;
                 periodEnd = reportExportData.Data.First().PeriodEnd;
            }

            var html = await _templateRenderer.RenderTemplateAsync(
              "ExportTransactionCategoryBudgetVsActualOutflow-template.hbs",
              new { data = reportExportData.Data, periodStart, periodEnd, periodType = request.PeriodType.ToString()}
          );

            // Generate PDF
            return await _pdfGenerator.GeneratePdfAsync(html);
        }
    }
}

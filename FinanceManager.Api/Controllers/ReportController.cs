using FinanceManager.Application.Features.TransactionRecords.Queries.ExportToPdf;
using FinanceManager.Application.FeaturesDapper.Reports.Queries.TransactionCategoryBudgetVsActualOutflow;
using FinanceManager.Application.FeaturesDapper.Reports.Queries.TransactionCategoryBudgetVsActualOutflow.ExportToPdf;
using FinanceManager.Application.FeaturesDapper.Reports.Queries.TransactionRecordSummaryByCategoryType;
using FinanceManager.Application.FeaturesDapper.Reports.Queries.TransactionRecordSummaryByPaymentMethod;
using FinanceManager.Application.FeaturesDapper.Reports.Queries.TransactionRecordSummaryByTransactionCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace FinanceManager.Api.Controllers
{
    [Route("api/reports/transaction-record")]
    [ApiController]
    public class ReportController(ISender sender) : ControllerBase
    {

        [HttpPost("summary/transaction-category")]
        public async Task<IActionResult> GetTransactionRecordSummaryByCategory(TransactionRecordSummaryByTransactionCategoryQuery query)
            {
                var response = await sender.Send(query);
                return Ok(response);
            }

    
        [HttpPost("summary/category-type")]
        public async Task<IActionResult> GetIncomeExpenseSummary( TransactionRecordSummaryByCategoryTypeQuery query)
        {
            var response = await sender.Send(query);
            return Ok(response);

        }

        [HttpPost("summary/payment-method")]
        public async Task<IActionResult> GetTransactionRecordSummaryByPaymentMethod(TransactionRecordSummaryByPaymentMethodQuery query)
        {
            var response = await sender.Send(query);
            return Ok(response);

        }

        [HttpPost("transaction-category-budget-vs-actual-outflow")]

        public async Task<IActionResult> GetTransactionCategoryBudgetVsActualOutflow(TransactionCategoryBudgetVsActualOutflowQuery query)
        {
            var response = await sender.Send(query);
            return Ok(response);
        }

        [HttpPost("transaction-category-budget-vs-actual-outflow/export/pdf")]
        public async Task<IActionResult> ExportToPdf(ExportTransactionCategoryBudgetVsActualOutflowQuery query)
        {
            var pdfBytes = await sender.Send(query);
            return File(pdfBytes, "application/pdf", $"TransactionCategoryBudgetVsActualOutflow-{DateTime.UtcNow:yyyyMMddHHmmss}.pdf");
        }




    }
}

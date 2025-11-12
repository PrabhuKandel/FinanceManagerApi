using FinanceManager.Application.FeaturesDapper.Reports.TransactionCategoryBudgetVsActualOutflow.ExportToExcel;
using FinanceManager.Application.FeaturesDapper.Reports.TransactionCategoryBudgetVsActualOutflow.ExportToPdf;
using FinanceManager.Application.FeaturesDapper.Reports.TransactionCategoryBudgetVsActualOutflow.Queries;
using FinanceManager.Application.FeaturesDapper.Reports.TransactionRecordSummaryByCategoryType.Queries;
using FinanceManager.Application.FeaturesDapper.Reports.TransactionRecordSummaryByPaymentMethod.ExportToExcel;
using FinanceManager.Application.FeaturesDapper.Reports.TransactionRecordSummaryByPaymentMethod.Queries;
using FinanceManager.Application.FeaturesDapper.Reports.TransactionRecordSummaryByTransactionCategory.ExportToExcel;
using FinanceManager.Application.FeaturesDapper.Reports.TransactionRecordSummaryByTransactionCategory.Queries;
using FinanceManager.Infrastructure.Authorization.Permissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FinanceManager.Api.Controllers{
    [Authorize(Policy =PermissionConstants.Report.View)]
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
        [Authorize(Policy = PermissionConstants.Report.Export)]
        [HttpPost("summary/transaction-category/export/excel")]
        public async Task<IActionResult> ExportToExcel(ExportExcelTransactionRecordSummaryByTransactionCategoryQuery query)
        {
            var excelBytes = await sender.Send(query);
            // Return as a file
            return File(
                excelBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "TransactionSummaryByTransactionCategory.xlsx"
            );
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
        [Authorize(Policy = PermissionConstants.Report.Export)]
        [HttpPost("summary/payment-method/export/excel")]
        public async Task<IActionResult> ExportToExcel(ExportExcelTransactionRecordSummaryByPaymentMethodQuery query)
        {
            var excelBytes = await sender.Send(query);
            // Return as a file
            return File(
                excelBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "TransactionSummaryByPaymentMethod.xlsx"
            );
        }

        [HttpPost("transaction-category-budget-vs-actual-outflow")]

        public async Task<IActionResult> GetTransactionCategoryBudgetVsActualOutflow(TransactionCategoryBudgetVsActualOutflowQuery query)
        {
            var response = await sender.Send(query);
            return Ok(response);
        }

        [Authorize(Policy = PermissionConstants.Report.Export)]
        [HttpPost("transaction-category-budget-vs-actual-outflow/export/pdf")]
        public async Task<IActionResult> ExportToPdf(ExportTransactionCategoryBudgetVsActualOutflowQuery query)
        {
            var pdfBytes = await sender.Send(query);
            return File(pdfBytes, "application/pdf", $"TransactionCategoryBudgetVsActualOutflow-{DateTime.UtcNow:yyyyMMddHHmmss}.pdf");
        }

        [Authorize(Policy = PermissionConstants.Report.Export)]
        [HttpPost("transaction-category-budget-vs-actual-outflow/export/excel")]
        public async Task<IActionResult> ExportToExcel(ExportExcelTransactionCategoryBudgetVsActualOutflowQuery query)
        {
            var excelBytes = await sender.Send(query);
            // Return as a file
            return File(
                excelBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "TransactionCategoryBudgetVsActualReport.xlsx"
            );
        }


    }
}

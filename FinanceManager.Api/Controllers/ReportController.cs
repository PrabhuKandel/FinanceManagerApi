using FinanceManager.Application.Features.Report.Commands.TransactionRecordSummaryByIncomeExpense;
using FinanceManager.Application.FeaturesDapper.Reports.Queries.TransactionRecordSummaryByTransactionCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public async Task<IActionResult> GetIncomeExpenseSummary( TransactionRecordSummaryByCategoryTypeCommand command)
        {
            var response = await sender.Send(command);
            return Ok(response);

        }

    }
}

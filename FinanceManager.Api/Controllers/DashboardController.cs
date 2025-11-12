using FinanceManager.Application.FeaturesDapper.Dashboards.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Api.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController(ISender sender) : ControllerBase
    {
        [HttpGet("summary")]
        public async Task<IActionResult> GetDashboardSummary()
        {
            return Ok(await sender.Send(new GetDashboardSummaryQuery()));
        }
    }
}

using FinanceManager.Application.Features.Budgets.Commands.Create;
using FinanceManager.Application.Features.PaymentMethods.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Api.Controllers
{
    [Route("api/budget")]
    [ApiController]
    public class BudgetController(ISender sender) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Create(CreateBudgetCommand command)
        {

            var response = await sender.Send(command);
            return Ok(response);

        }

    }
}

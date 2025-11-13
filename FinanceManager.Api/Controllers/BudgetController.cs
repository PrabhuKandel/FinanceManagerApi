using FinanceManager.Application.Features.Budgets.Commands.Create;
using FinanceManager.Application.Features.Budgets.Queries.GetAll;
using FinanceManager.Application.Features.PaymentMethods.Commands.Create;
using FinanceManager.Infrastructure.Authorization.Permissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Api.Controllers
{
    [Route("api/budget")]
    [ApiController]
    public class BudgetController(ISender sender) : ControllerBase
    {

        [Authorize(Policy =PermissionConstants.Budeget.Create)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBudgetCommand command)
        {

            var response = await sender.Send(command);
            return Ok(response);

        }


        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
          
            var response = await sender.Send(new GetAllBudgetsQuery());
            return Ok(response);

        }

    }
}

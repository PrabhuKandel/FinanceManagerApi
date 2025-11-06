using FinanceManager.Application.Features.TransactionCategories.Commands.Create;
using FinanceManager.Application.Features.TransactionCategories.Commands.Delete;
using FinanceManager.Application.Features.TransactionCategories.Commands.Update;
using FinanceManager.Application.Features.TransactionCategories.Queries.GetAll;
using FinanceManager.Application.Features.TransactionCategories.Queries.GetById;
using FinanceManager.Application.FeaturesStoredProcedure.TransactionCategory.Commands.CreateTransactionCategory;
using FinanceManager.Application.FeaturesStoredProcedure.TransactionCategory.Commands.DeleteTransactionCategory;
using FinanceManager.Application.FeaturesStoredProcedure.TransactionCategory.Commands.UpdateTransactionCategory;
using FinanceManager.Infrastructure.Authorization.Permissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionCategoryController : ControllerBase
    {
        private readonly IMediator mediator;

        public TransactionCategoryController(IMediator _mediator)
        {
            mediator = _mediator;
        }

        [HttpGet]
        [Authorize(Policy = PermissionConstants.TransactionCategory.View)]
        public async Task<IActionResult> GetAll()
        {
            var response = await mediator.Send(new GetAllTransactionCategoriesQuery());

            return Ok(response);
        }


        [HttpGet("{id}")]
        [Authorize(Policy = PermissionConstants.TransactionCategory.View)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await mediator.Send(new GetTransactionCategoryByIdQuery(id));

            return Ok(response);


        }   
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [Authorize(Policy = PermissionConstants.TransactionCategory.Create)]
        public async Task<IActionResult> Create(CreateTransactionCategoryCommand createCommand)
        {

            var response = await mediator.Send(createCommand);
            return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);

        }
        [HttpPost("spCreate")]

        public async Task<IActionResult> SpCreate(CreateTransactionCategorySpCommand createCommand)
        {
            var response = await mediator.Send(createCommand);

            return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);

        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        [Authorize(Policy = PermissionConstants.TransactionCategory.Update)]
        public async Task<IActionResult> Update(UpdateTransactionCategoryCommand updateCommand)
        {
            var response = await mediator.Send(updateCommand);
            return Ok(response);
            

        }

        [HttpPut("spUpdate/{id}")]

        public async Task<IActionResult> SpUpdate(UpdateTransactionCategorySpCommand updateCommand)
        {
            var response = await mediator.Send(updateCommand);

            return Ok(response);

        }



        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        [Authorize(Policy = PermissionConstants.TransactionCategory.Delete)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await mediator.Send(new DeleteTransactionCategoryCommand(id));
            return Ok(response);
         



        }


        [HttpDelete("spDelete/{id}")]
        public async Task<IActionResult> SpDelete(Guid id)
        {
            var response = await mediator.Send( new DeleteTransactionCategorySpCommand(id));
            return Ok(response);




        }
    }
}

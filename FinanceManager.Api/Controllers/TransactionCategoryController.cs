using System.ComponentModel.DataAnnotations;
using Azure;
using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Features.TransactionCategories.Commands;
using FinanceManager.Application.Features.TransactionCategories.Queries;
using FinanceManager.Application.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionCategoryController : ControllerBase
    {
        private readonly IMediator mediator;

        public TransactionCategoryController(IMediator _mediator)
        {
            mediator = _mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await mediator.Send(new GetAllTransactionCategoriesQuery());

            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await mediator.Send(new GetTransactionCategoryByIdQuery(id));

            return Ok(response);


        }   
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(TransactionCategoryCreateDto transactionCategoryCreateDto)
        {

            var response = await mediator.Send(new CreateTransactionCategoryCommand(transactionCategoryCreateDto));
            return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id,  TransactionCategoryUpdateDto transactionCategoryUpdateDto)
        {
            var response = await mediator.Send(new UpdateTransactionCategoryCommand(id,transactionCategoryUpdateDto));
            return Ok(response);
            


        }



        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await mediator.Send(new DeleteTransactionCategoryCommand(id));
            return Ok(response);
         



        }
    }
}

using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Application.Features.TransactionRecords.Commands;
using FinanceManager.Application.Features.TransactionRecords.Queries;
using FinanceManager.Application.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionRecordController(IMediator _mediator) : ControllerBase
    {
  

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            
            var response = await _mediator.Send(new GetAllTransactionRecordsQuery());
            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
           
            var response = await _mediator.Send(new GetTransactionRecordByIdQuery(id));

            return Ok(response);


        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionRecordCreateDto transactionRecordCreateDto)
        {
            
            var response = await _mediator.Send(new CreateTransactionRecordCommand(transactionRecordCreateDto));
            return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);

        }

        [HttpPut("{id}")]   
        public async Task<IActionResult> Update(Guid id, [FromBody] TransactionRecordUpdateDto transactionRecordUpdateDto)
        {

          
            var response = await _mediator.Send(new UpdateTransactionRecordCommand(id, transactionRecordUpdateDto));
            return Ok(response);
            


        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] TransactionRecordPatchDto transactionRecordPatchDto)
        {

            var response = await _mediator.Send(new PatchTransactionRecordCommand(id, transactionRecordPatchDto));
            return Ok(response);
        }



            [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
          
            var response = await _mediator.Send(new DeleteTransactionRecordCommand(id));
            return Ok(response);
         


        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterTransactionRecords(
            [FromQuery] decimal? minAmount,
            [FromQuery] decimal? maxAmount,
            [FromQuery] Guid? transacionCategory,
            [FromQuery] Guid? paymentMethod,
             [FromQuery] DateTime transactionDate
            )
        {

            var response = await _mediator.Send( new FilterTransactionRecordsQuery(minAmount, maxAmount, transacionCategory, paymentMethod, transactionDate));
            return Ok(response);
        }


    }
}

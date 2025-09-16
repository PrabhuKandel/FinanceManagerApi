using FinanceManager.Application.Features.TransactionRecords.Commands;
using FinanceManager.Application.Features.TransactionRecords.Queries;
using FinanceManager.Application.FeaturesDapper.TransactionRecords.Queries.GetAllTransactionRecord;
using FinanceManager.Application.FeaturesDapper.TransactionRecords.Queries.GetTransactionRecordById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionRecordController : ControllerBase
    {
        private readonly IMediator mediator;

        public TransactionRecordController(IMediator _mediator)
        {
            mediator = _mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            
            var response = await mediator.Send(new GetAllTransactionRecordsQuery());
            return Ok(response);
        }


        [HttpGet("dapperGetAll")]
        public async Task<IActionResult> DapperGetAll()
        {

            var response = await mediator.Send(new GetAllTransactionRecordsDapperQuery());
            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
           
            var response = await mediator.Send(new GetTransactionRecordByIdQuery(id));

            return Ok(response);


        }


        [HttpGet("dapperGetById/{id}")]
        public async Task<IActionResult> DapperGetById(Guid id)
        {

            var response = await mediator.Send(new GetTransactionRecordByIdDapperQuery(id));

            return Ok(response);


        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTransactionRecordCommand createCommand)
        {
            
            var response = await mediator.Send(createCommand);
            return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);

        }


        [HttpPut("{id}")]   
        public async Task<IActionResult> Update(UpdateTransactionRecordCommand updateCommand)
        {

          
            var response = await mediator.Send(updateCommand);
            return Ok(response);
            


        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(PatchTransactionRecordCommand patchCommand)
        {

            var response = await mediator.Send(patchCommand);
            return Ok(response);
        }



            [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
          
            var response = await mediator.Send(new DeleteTransactionRecordCommand(id));
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

            var response = await mediator.Send( new FilterTransactionRecordsQuery(minAmount, maxAmount, transacionCategory, paymentMethod, transactionDate));
            return Ok(response);
        }


    }
}

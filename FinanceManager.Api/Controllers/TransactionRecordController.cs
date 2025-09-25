using FinanceManager.Application.Features.TransactionRecords.Commands;
using FinanceManager.Application.Features.TransactionRecords.Queries;
using FinanceManager.Application.FeaturesDapper.TransactionRecords.Commands.CreateTransactionRecord;
using FinanceManager.Application.FeaturesDapper.TransactionRecords.Commands.DeleteTransactionRecord;
using FinanceManager.Application.FeaturesDapper.TransactionRecords.Commands.PatchTransactionRecord;
using FinanceManager.Application.FeaturesDapper.TransactionRecords.Commands.UpdateTransactionRecord;
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
        public async Task<IActionResult> GetAll([FromQuery]GetAllTransactionRecordsQuery query)
        {
            
            var response = await mediator.Send(query);
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

        [HttpPost("dapperCreate")]
        public async Task<IActionResult> DapperCreate(CreateTransactionRecordDapperCommand createCommand)
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


        [HttpPut("dapperUpdate/{id}")]
        public async Task<IActionResult> DapperUpdate(UpdateTransactionRecordDapperCommand updateCommand)
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


        [HttpPatch("dapperPatch/{id}")]
        public async Task<IActionResult> DapperPatch(PatchTransactionRecordDapperCommand patchCommand)
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

        [HttpDelete("dapperDelete/{id}")]
        public async Task<IActionResult> DapperDelete(Guid id)
        {

            var response = await mediator.Send(new DeleteTransactionRecordDapperCommand(id));
            return Ok(response);

        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterTransactionRecords(
            [FromQuery] decimal? minAmount,
            [FromQuery] decimal? maxAmount,
            [FromQuery] Guid? transacionCategoryId,
            [FromQuery] Guid? paymentMethodId,
             [FromQuery] DateTime transactionDate
            )
        {

            var response = await mediator.Send( new FilterTransactionRecordsQuery(minAmount, maxAmount, transacionCategoryId, paymentMethodId, transactionDate));
            return Ok(response);
        }


    }
}

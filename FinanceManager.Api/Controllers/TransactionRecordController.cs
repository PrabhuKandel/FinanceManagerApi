using System.Text.Json;
using Azure;
using FinanceManager.Api.ModelBinder;
using FinanceManager.Application.Features.TransactionRecords.Commands.Create;
using FinanceManager.Application.Features.TransactionRecords.Commands.Delete;
using FinanceManager.Application.Features.TransactionRecords.Commands.DeleteAttachment;
using FinanceManager.Application.Features.TransactionRecords.Commands.PatchApprovalStatus;
using FinanceManager.Application.Features.TransactionRecords.Commands.PatchTransactionRecord;
using FinanceManager.Application.Features.TransactionRecords.Commands.Update;
using FinanceManager.Application.Features.TransactionRecords.Queries.ExportToExcel;
using FinanceManager.Application.Features.TransactionRecords.Queries.ExportToPdf;
using FinanceManager.Application.Features.TransactionRecords.Queries.GetAll;
using FinanceManager.Application.Features.TransactionRecords.Queries.GetById;
using FinanceManager.Application.FeaturesDapper.TransactionRecords.Commands.CreateTransactionRecord;
using FinanceManager.Application.FeaturesDapper.TransactionRecords.Commands.DeleteTransactionRecord;
using FinanceManager.Application.FeaturesDapper.TransactionRecords.Commands.PatchTransactionRecord;
using FinanceManager.Application.FeaturesDapper.TransactionRecords.Commands.UpdateTransactionRecord;
using FinanceManager.Application.FeaturesDapper.TransactionRecords.Queries.GetAllTransactionRecord;
using FinanceManager.Application.FeaturesDapper.TransactionRecords.Queries.GetTransactionRecordById;
using FinanceManager.Infrastructure.Authorization.Permissions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FinanceManager.Api.Controllers
{
    //[Authorize]
    [Route("api/transaction-records")]
    [ApiController]
    public class TransactionRecordController : ControllerBase
    {
        private readonly IMediator mediator;

        public TransactionRecordController(IMediator _mediator)
        {
            mediator = _mediator;
        }
        [Authorize(Policy = PermissionConstants.TransactionRecordPermissions.View)]

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll(GetAllTransactionRecordsQuery query)
        {
            
            var response = await mediator.Send(query);
            return Ok(response);
        }


        [HttpPost("dapper-get-all")]
        public async Task<IActionResult> DapperGetAll( GetAllTransactionRecordsDapperQuery query)
        {

            var response = await mediator.Send(query);
            return Ok(response);
        }

        [Authorize(Policy = PermissionConstants.TransactionRecordPermissions.View)]
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



        [Authorize(Policy = PermissionConstants.TransactionRecordPermissions.Create)]
        [HttpPost]
        public async Task<IActionResult> Create(
        [FromForm]
        [ModelBinder(BinderType = typeof(TransactionRecordCommandModelBinder))]
        CreateTransactionRecordCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("dapperCreate")]
        public async Task<IActionResult> DapperCreate(CreateTransactionRecordDapperCommand createCommand)
        {

            var response = await mediator.Send(createCommand);
            return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);

        }


        [Authorize(Policy = PermissionConstants.TransactionRecordPermissions.Update)]
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

        [Authorize(Policy = PermissionConstants.TransactionRecordPermissions.Approve)]
        [HttpPatch("approval")]
        public async Task<IActionResult> PatchApprovalStatus(PatchTransactionRecordApprovalStatusCommand patchCommand)
        {
            var response = await mediator.Send(patchCommand);
            return Ok(response);
        }


        [Authorize(Policy = PermissionConstants.TransactionRecordPermissions.Update)]
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

        [Authorize(Policy = PermissionConstants.TransactionRecordPermissions.Delete)]
        [HttpDelete("{id}")]
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

        [Authorize(Policy = PermissionConstants.TransactionRecordPermissions.Delete)]
        [HttpDelete("attachments")]
        public async Task<IActionResult> DeleteAttachments(DeleteTransactionRecordAttachmentCommand command)
        {
    
            var response = await mediator.Send(command);
             return Ok(response);

        }

        [Authorize(Policy = PermissionConstants.TransactionRecordPermissions.Export)]
        [HttpPost("export/excel")]
        public async Task<IActionResult> ExportTransactionRecords(ExportTransactionRecordsQuery query)
        {
            var fileBytes = await mediator.Send(query);
            return File(
                fileBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "TransactionRecords.xlsx"
            );
        }

        [Authorize(Policy = PermissionConstants.TransactionRecordPermissions.Export)]
        [HttpPost("export/pdf")]
        public async Task<IActionResult> ExportToPdf(ExportTransactionRecordsToPdfQuery query)
        {
            var pdfBytes = await mediator.Send(query);
            return File(pdfBytes, "application/pdf", $"transactions-{DateTime.UtcNow:yyyyMMddHHmmss}.pdf");
        }



    }
}

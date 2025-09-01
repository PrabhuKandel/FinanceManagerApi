using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Azure;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionRecordController : ControllerBase
    {
        private readonly ITransactionRecordService _transactionRecordService;
        public TransactionRecordController(ITransactionRecordService transactionRecordService)
        {
            _transactionRecordService = transactionRecordService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User?.FindFirst("userId")?.Value;
            var response = await _transactionRecordService.GetAllTransactionRecordsAsync(userId);

            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = User?.FindFirst("userId")?.Value;
            var response = await _transactionRecordService.GetTransactionRecordByIdAsync(id,userId);

            return Ok(response);


        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionRecordCreateDto transactionRecordCreateDto)
        {
            var userId = User?.FindFirst("userId")?.Value;
            var response = await _transactionRecordService.AddTransactionRecordAsync(transactionRecordCreateDto,userId);
            return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);

        }

        [HttpPut("{id}")]   
        public async Task<IActionResult> Update(Guid id, [FromBody] TransactionRecordUpdateDto transactionRecordUpdateDto)
        {

            var userId = User?.FindFirst("userId")?.Value;
            var response = await _transactionRecordService.UpdateTransactionRecordAsync(id, transactionRecordUpdateDto ,userId);
            return Ok(response);
            


        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User?.FindFirst("userId")?.Value;
            var response = await _transactionRecordService.DeleteTransactionRecordAsync(id,userId);
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
            var userId = User?.FindFirst("userId")?.Value;
            var response = await _transactionRecordService.FilterTransactionRecordsAsync(userId,minAmount, maxAmount, transacionCategory, paymentMethod, transactionDate);
            return Ok(response);
        }


    }
}

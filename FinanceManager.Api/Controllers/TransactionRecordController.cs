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
            
            var response = await _transactionRecordService.GetAllTransactionRecordsAsync();

            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
           
            var response = await _transactionRecordService.GetTransactionRecordByIdAsync(id);

            return Ok(response);


        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionRecordCreateDto transactionRecordCreateDto)
        {
            
            var response = await _transactionRecordService.AddTransactionRecordAsync(transactionRecordCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);

        }

        [HttpPut("{id}")]   
        public async Task<IActionResult> Update(Guid id, [FromBody] TransactionRecordUpdateDto transactionRecordUpdateDto)
        {

          
            var response = await _transactionRecordService.UpdateTransactionRecordAsync(id, transactionRecordUpdateDto );
            return Ok(response);
            


        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
          
            var response = await _transactionRecordService.DeleteTransactionRecordAsync(id);
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
            var response = await _transactionRecordService.FilterTransactionRecordsAsync(minAmount, maxAmount, transacionCategory, paymentMethod, transactionDate);
            return Ok(response);
        }


    }
}

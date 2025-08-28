using Azure;
using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionCategoryController : ControllerBase
    {
        private readonly ITransactionCategoryService _transactionCategoryService;
        public TransactionCategoryController(ITransactionCategoryService transactionCategoryService)
        {
            _transactionCategoryService = transactionCategoryService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _transactionCategoryService.GetAllTransactionCategoriesAsync();

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _transactionCategoryService.GetTransactionCategoryByIdAsync(id);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);


        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionCategoryCreateDto transactionCategoryCreateDto)
        {

            var response = await _transactionCategoryService.AddTransactionCategoryAsync(transactionCategoryCreateDto);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TransactionCategoryUpdateDto transactionCategoryUpdateDto)
        {
            var response = await _transactionCategoryService.UpdateTransactionCategoryAsync(id, transactionCategoryUpdateDto);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }


        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var response = await _transactionCategoryService.DeleteTransactionCategoryAsync(id);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }



        }
    }
}

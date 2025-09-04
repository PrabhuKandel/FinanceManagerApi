using System.ComponentModel.DataAnnotations;
using Azure;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;


namespace FinanceManager.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController(IPaymentMethodService paymentMethodService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await paymentMethodService.GetAllPaymentMethodsAsync();

            return Ok(response);
        }


        [HttpGet("{id}")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var response = await paymentMethodService.GetPaymentMethodByIdAsync(id);

            return Ok(response);


        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] PaymentMethodCreateDto paymentMethodCreateDto)
        {
            Log.Information("Payment Create Request: {@Request}", paymentMethodCreateDto);

            var response = await paymentMethodService.AddPaymentMethodAsync(paymentMethodCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PaymentMethodUpdateDto paymentMethodUpdateDto)
        {
           
            var response = await paymentMethodService.UpdatePaymentMethodAsync(id, paymentMethodUpdateDto);
            return Ok(response);
            


        }



        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var response = await paymentMethodService.DeletePaymentMethodAsync(id);
            return Ok(response);
         



        }
    }
}

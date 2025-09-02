using System.ComponentModel.DataAnnotations;
using Azure;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;
        public PaymentMethodController(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _paymentMethodService.GetAllPaymentMethodsAsync();

            return Ok(response);
        }


        [HttpGet("{id}")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var response = await _paymentMethodService.GetPaymentMethodByIdAsync(id);

            return Ok(response);


        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] PaymentMethodCreateDto paymentMethodCreateDto)
        {

            var response = await _paymentMethodService.AddPaymentMethodAsync(paymentMethodCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PaymentMethodUpdateDto paymentMethodUpdateDto)
        {
           
            var response = await _paymentMethodService.UpdatePaymentMethodAsync(id, paymentMethodUpdateDto);
            return Ok(response);
            


        }



        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var response = await _paymentMethodService.DeletePaymentMethodAsync(id);
            return Ok(response);
         



        }
    }
}

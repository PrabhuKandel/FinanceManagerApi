using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Features.PaymentMethods.Commands;
using FinanceManager.Application.Features.PaymentMethods.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;


namespace FinanceManager.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController( IMediator _mediator) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllPaymentMethodsQuery());

            return Ok(response);
        }


        [HttpGet("{id}")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetPaymentMethodByIdQuery(id));

            return Ok(response);


        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] PaymentMethodCreateDto paymentMethodCreateDto)
        {
            Log.Information("Payment Create Request: {@Request}", paymentMethodCreateDto);

            var response = await _mediator.Send(new CreatePaymentMethodCommand(paymentMethodCreateDto));
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PaymentMethodUpdateDto paymentMethodUpdateDto)
        {
            var response = await _mediator.Send(new UpdatePaymentMethodCommand(id, paymentMethodUpdateDto));
            return Ok(response);
            
        }



        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _mediator.Send(new DeletePaymentMethodCommand(id));
            return Ok(response);


        }
    }
}

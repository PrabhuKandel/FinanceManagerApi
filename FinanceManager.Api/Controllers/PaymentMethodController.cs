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
    public class PaymentMethodController : ControllerBase
    {
        private readonly IMediator mediator;

        public PaymentMethodController( IMediator _mediator)
        {
            mediator = _mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await mediator.Send(new GetAllPaymentMethodsQuery());

            return Ok(response);
        }


        [HttpGet("{id}")]

        public async Task<IActionResult> GetById( Guid id)
        {
            var response = await mediator.Send(new GetPaymentMethodByIdQuery(id));

            return Ok(response);


        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(PaymentMethodCreateDto paymentMethodCreateDto)
        {
            

            var response = await mediator.Send(new CreatePaymentMethodCommand(paymentMethodCreateDto));
            return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, PaymentMethodUpdateDto paymentMethodUpdateDto)
        {
            var response = await mediator.Send(new UpdatePaymentMethodCommand(id, paymentMethodUpdateDto));
            return Ok(response);
            
        }



        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await mediator.Send(new DeletePaymentMethodCommand(id));
            return Ok(response);


        }
    }
}

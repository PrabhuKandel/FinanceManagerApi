using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Features.PaymentMethods.Commands;
using FinanceManager.Application.Features.PaymentMethods.Queries;
using FinanceManager.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;


namespace FinanceManager.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController: ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ApplicationDbContext context;

        public PaymentMethodController( IMediator _mediator, ApplicationDbContext _context)
        {
            mediator = _mediator;
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await mediator.Send(new GetAllPaymentMethodsQuery());

            return Ok(response);
        }

        [HttpGet("spGetAll")]
        public async Task<IActionResult> SpGetAll()
        {
            var response = await mediator.Send(new GetAllPaymentMethodsSpQuery());
            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById( Guid id)
        {
            var response = await mediator.Send(new GetPaymentMethodByIdQuery(id));

            return Ok(response);


        }



        [HttpGet("spGetById/{id}")]
        public async Task<IActionResult> SpGetById(Guid id)
        {
            var response = await mediator.Send(new GetPaymentMethodByIdSpQuery(id));

            return Ok(response);

        }




        [HttpPost]
     
        public async Task<IActionResult> Create(PaymentMethodCreateDto paymentMethodCreateDto)
        {

            var response = await mediator.Send(new CreatePaymentMethodCommand(paymentMethodCreateDto));
            return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);

        }

        [HttpPost("spCreate")]

        public async Task<IActionResult> SpCreate(PaymentMethodCreateDto paymentMethodCreateDto)
        {
            var response = await mediator.Send(new CreatePaymentMethodSpCommand(paymentMethodCreateDto));

            return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);

        }






        [HttpPut("{id}")]

        public async Task<IActionResult> Update(Guid id, PaymentMethodUpdateDto paymentMethodUpdateDto)
        {
            var response = await mediator.Send(new UpdatePaymentMethodCommand(id, paymentMethodUpdateDto));
            return Ok(response);
            
        }


        [HttpPut("spUpdate/{id}")]

        public  async Task<IActionResult> SpUpdate(Guid id, PaymentMethodUpdateDto paymentMethodUpdateDto)
        {
            var response = await mediator.Send(new UpdatePaymentMethodSpCommand(id, paymentMethodUpdateDto));
            return Ok(response);

        }




        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await mediator.Send(new DeletePaymentMethodCommand(id));
            return Ok(response);


        }


        [HttpDelete("spDelete/{id}")]


        public async Task<IActionResult> SpDelete(Guid id)
        {
            var response = await mediator.Send(new DeletePaymentMethodSpCommand(id));
            return Ok(response);


        }




    }
}

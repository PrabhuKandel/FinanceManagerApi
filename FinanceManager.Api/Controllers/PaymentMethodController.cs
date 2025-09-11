using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Features.PaymentMethods.Commands;
using FinanceManager.Application.Features.PaymentMethods.Queries;
using FinanceManager.Application.FeaturesStoredProcedure.PaymentMethods.Commands.CreatePaymentMethod;
using FinanceManager.Application.FeaturesStoredProcedure.PaymentMethods.Commands.DeletePaymentMethod;
using FinanceManager.Application.FeaturesStoredProcedure.PaymentMethods.Commands.UpdatePaymentMethod;
using FinanceManager.Application.FeaturesStoredProcedure.PaymentMethods.Queries.GellAllPaymentMethod;
using FinanceManager.Application.FeaturesStoredProcedure.PaymentMethods.Queries.GetPaymentMethodById;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace FinanceManager.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController: ControllerBase
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
            var response = await mediator.Send(new CreatePaymentMethodSpCommand(paymentMethodCreateDto.Name, paymentMethodCreateDto.Description, paymentMethodCreateDto.IsActive));

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
            var response = await mediator.Send(new UpdatePaymentMethodSpCommand(id, paymentMethodUpdateDto.Name, paymentMethodUpdateDto.Description, paymentMethodUpdateDto.IsActive));
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

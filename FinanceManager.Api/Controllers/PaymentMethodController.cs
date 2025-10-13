using FinanceManager.Application.Features.PaymentMethods.Commands.Create;
using FinanceManager.Application.Features.PaymentMethods.Commands.Delete;
using FinanceManager.Application.Features.PaymentMethods.Commands.Update;
using FinanceManager.Application.Features.PaymentMethods.Queries;
using FinanceManager.Application.FeaturesDapper.PaymentMethods.Commands.CreatePaymentMethod;
using FinanceManager.Application.FeaturesDapper.PaymentMethods.Commands.DeletePaymentMethod;
using FinanceManager.Application.FeaturesDapper.PaymentMethods.Commands.UpdatePaymentMethod;
using FinanceManager.Application.FeaturesDapper.PaymentMethods.Queries.GellAllPaymentMethod;
using FinanceManager.Application.FeaturesDapper.PaymentMethods.Queries.GetPaymentMethodById;
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


        [HttpGet("dapperGetAll")]
        public async Task<IActionResult> DapperGetAll()
        {
            var response = await mediator.Send(new GetAllPaymentMethodsDapperQuery());
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

        [HttpGet("dapperGetById/{id}")]
        public async Task<IActionResult> DapperGetById(Guid id)
        {
            var response = await mediator.Send(new GetPaymentMethodByIdDapperQuery(id));

            return Ok(response);

        }



        [HttpPost]
        public async Task<IActionResult> Create(CreatePaymentMethodCommand createCommand)
        {

            var response = await mediator.Send(createCommand);
            return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);

        }

        [HttpPost("spCreate")]

        public async Task<IActionResult> SpCreate(CreatePaymentMethodSpCommand createCommand)
        {
            var response = await mediator.Send(createCommand);

            return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response );

        }

        [HttpPost("dapperCreate")]

        public async Task<IActionResult> DapperCreate(CreatePaymentMethodDapperCommand createCommand)
        {
            var response = await mediator.Send(createCommand);

            return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(UpdatePaymentMethodCommand updateCommand)
        {
            var response = await mediator.Send(updateCommand);
            return Ok(response);
            
        }


        [HttpPut("spUpdate/{id}")]

        public  async Task<IActionResult> SpUpdate(UpdatePaymentMethodSpCommand updateCommand)
        {
            var response = await mediator.Send(updateCommand);
            return Ok(response);

        }

        [HttpPut("dapperUpdate/{id}")]

        public async Task<IActionResult> DapperUpdate(UpdatePaymentMethodDapperCommand updateCommand)
        {
            var response = await mediator.Send(updateCommand);
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




        [HttpDelete("dapperDelete/{id}")]


        public async Task<IActionResult> DapperDelete(Guid id)
        {
            var response = await mediator.Send(new DeletePaymentMethodDapperCommand(id));
            return Ok(response);


        }



    }
}

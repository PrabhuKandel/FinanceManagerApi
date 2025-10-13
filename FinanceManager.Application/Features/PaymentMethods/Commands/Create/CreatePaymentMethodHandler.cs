using Ardalis.GuardClauses;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Features.PaymentMethods.Mapping;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Mapping;
using MediatR;



namespace FinanceManager.Application.Features.PaymentMethods.Commands.Create
{
    public  class CreatePaymentMethodHandler : IRequestHandler<CreatePaymentMethodCommand, OperationResult<PaymentMethodResponseDto>>
    {
        private readonly IApplicationDbContext context;

        public CreatePaymentMethodHandler(IApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<OperationResult<PaymentMethodResponseDto>> Handle(CreatePaymentMethodCommand request, CancellationToken cancellationToken)
        {
           
            var entity = request.ToEntity();
            await context.PaymentMethods.AddAsync(entity);
            await context.SaveChangesAsync(cancellationToken);  
          
            return new OperationResult<PaymentMethodResponseDto>
            {

                Message = "New payment method added",
                Data = entity.ToResponseDto()
            };


        }
    }

}
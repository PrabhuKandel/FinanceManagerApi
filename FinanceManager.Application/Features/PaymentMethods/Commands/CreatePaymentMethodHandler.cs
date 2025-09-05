using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Mapping;
using FinanceManager.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.PaymentMethods.Commands
{
    public  class CreatePaymentMethodHandler : IRequestHandler<CreatePaymentMethodCommand, OperationResult<PaymentMethodResponseDto>>
    {
        private readonly ApplicationDbContext context;

        public CreatePaymentMethodHandler(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<OperationResult<PaymentMethodResponseDto>> Handle(CreatePaymentMethodCommand request, CancellationToken cancellationToken)
        {


            if (await  context.PaymentMethods.AnyAsync(c => c.Name == request.paymentMethod.Name))
                throw new BusinessValidationException("Payment method with this name already exists.");

            var entity = request.paymentMethod.ToEntity();
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
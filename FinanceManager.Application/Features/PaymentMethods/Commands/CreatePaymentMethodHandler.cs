using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Mapping;
using FinanceManager.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.PaymentMethods.Commands
{
    public  class CreatePaymentMethodHandler(ApplicationDbContext _context) : IRequestHandler<CreatePaymentMethodCommand, OperationResult<PaymentMethodResponseDto>>
    {

        public async Task<OperationResult<PaymentMethodResponseDto>> Handle(CreatePaymentMethodCommand request, CancellationToken cancellationToken)
        {


            if (await  _context.PaymentMethods.AnyAsync(c => c.Name == request.paymentMethod.Name))
                throw new BusinessValidationException("Payment method with this name already exists.");

            var entity = request.paymentMethod.ToEntity();
            await _context.PaymentMethods.AddAsync(entity);
            await _context.SaveChangesAsync(cancellationToken);
          
            return new OperationResult<PaymentMethodResponseDto>
            {

                Message = "New payment method added",
                Data = entity.ToResponseDto()
            };


        }
    }

}
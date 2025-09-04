using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Mapping;
using FinanceManager.Domain.Entities;
using FinanceManager.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.PaymentMethods.Commands
{
    public  class CreatePaymentMethodHandler(ApplicationDbContext _context) : IRequestHandler<CreatePaymentMethodCommand, PaymentMethodResponseDto>
    {

        public async Task<PaymentMethodResponseDto> Handle(CreatePaymentMethodCommand request, CancellationToken cancellationToken)
        {


            if (await  _context.PaymentMethods.AnyAsync(c => c.Name == request.paymentMethod.Name))
                throw new CustomValidationException("Payment method with this name already exists.");

            var entity = request.paymentMethod.ToEntity();
            await _context.PaymentMethods.AddAsync(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.ToResponseDto();


        }
    }

}
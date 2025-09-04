using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Mapping;
using FinanceManager.Domain.Entities;
using FinanceManager.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.PaymentMethods.Commands.CreatePaymentMethod
{
    public  class UpdatePaymentMethodHandler(ApplicationDbContext _context) : IRequestHandler<UpdatePaymentMethodCommand, PaymentMethodResponseDto>
    {

        public async Task<PaymentMethodResponseDto> Handle(UpdatePaymentMethodCommand request, CancellationToken cancellationToken)
        {

            var paymentMethod = await _context.PaymentMethods.FindAsync(request.Id);
            if (paymentMethod == null)
            {
                throw new NotFoundException("Payment Method doesn't exist");
            }

            paymentMethod.UpdateEntity(request.paymentMethod);

            return paymentMethod.ToResponseDto();


        }
    }

}
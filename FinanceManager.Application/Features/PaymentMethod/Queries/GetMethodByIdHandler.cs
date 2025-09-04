using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Mapping;
using FinanceManager.Infrastructure.Data;
using MediatR;

namespace FinanceManager.Application.Features.PaymentMethod.Queries
{
    public class GetMethodByIdHandler(ApplicationDbContext _context) : IRequestHandler<GetPaymentMethodByIdQuery, PaymentMethodResponseDto>
    {


        public async Task<PaymentMethodResponseDto> Handle(GetPaymentMethodByIdQuery request, CancellationToken cancellationToken)
        {
            var paymentMethod = await _context.PaymentMethods.FindAsync(request.Id);
            if (paymentMethod == null)
            {
                throw new NotFoundException("Payment Method not found");
            }

            var paymentMethodDto = paymentMethod.ToResponseDto();
            
            return paymentMethod.ToResponseDto();
        }
    }
}
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Mapping;
using FinanceManager.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.PaymentMethods.Queries
{
    public class GetAllPaymentMethodsHandler(ApplicationDbContext _context) : IRequestHandler<GetAllPaymentMethodsQuery, OperationResult<IEnumerable<PaymentMethodResponseDto>>>
    {
    
        public async Task<OperationResult<IEnumerable<PaymentMethodResponseDto>>>Handle(GetAllPaymentMethodsQuery request, CancellationToken cancellationToken)
        {
            var paymentMethods = await _context.PaymentMethods.ToListAsync();
            var paymentMethodDtos = paymentMethods?.ToResponseDtoList();
            return new OperationResult<IEnumerable<PaymentMethodResponseDto>>
            {


                Data = paymentMethodDtos,
                Message = paymentMethodDtos.Any()
                     ? "Payment methods retrieved successfully"
                 : "  No payment methods "

            };
        }
    }
}
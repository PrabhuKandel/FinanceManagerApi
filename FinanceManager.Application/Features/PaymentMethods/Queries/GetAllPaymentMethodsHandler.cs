using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Mapping;
using FinanceManager.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.PaymentMethods.Queries
{
    public class GetAllPaymentMethodsHandler(ApplicationDbContext _context) : IRequestHandler<GetAllPaymentMethodsQuery, IEnumerable<PaymentMethodResponseDto>>
    {
    
        public async Task<IEnumerable<PaymentMethodResponseDto>>Handle(GetAllPaymentMethodsQuery request, CancellationToken cancellationToken)
        {
            var paymentMethods = await _context.PaymentMethods.ToListAsync();

            return paymentMethods.ToResponseDtoList();
        }
    }
}
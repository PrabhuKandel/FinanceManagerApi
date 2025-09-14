using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.PaymentMethods.Queries
{
    public class GetAllPaymentMethodsHandler : IRequestHandler<GetAllPaymentMethodsQuery, OperationResult<IEnumerable<PaymentMethodResponseDto>>>
    {
        private readonly IApplicationDbContext context;

        public GetAllPaymentMethodsHandler(IApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<OperationResult<IEnumerable<PaymentMethodResponseDto>>>Handle(GetAllPaymentMethodsQuery request, CancellationToken cancellationToken)
        {
            var paymentMethods = await context.PaymentMethods.ToListAsync();

            var paymentMethodDtos = paymentMethods.ToResponseDtoList();
            return new OperationResult<IEnumerable<PaymentMethodResponseDto>>
            {


                Data = paymentMethodDtos,
                Message =  "Payment methods retrieved successfully"
              

            };
        }
    }
}
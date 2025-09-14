using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Mapping;
using MediatR;

namespace FinanceManager.Application.Features.PaymentMethods.Queries
{
    public class GetPaymentMethodByIdHandler : IRequestHandler<GetPaymentMethodByIdQuery, OperationResult<PaymentMethodResponseDto>>
    {
        private readonly IApplicationDbContext context;

        public GetPaymentMethodByIdHandler(IApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<OperationResult<PaymentMethodResponseDto>> Handle(GetPaymentMethodByIdQuery request, CancellationToken cancellationToken)
        {
            var paymentMethod = await context.PaymentMethods.FindAsync(request.Id);
            return new OperationResult<PaymentMethodResponseDto>
            {

                Data = paymentMethod?.ToResponseDto(),
                Message = "Payment Method retrieved successfully"


            };
        }
    }
}
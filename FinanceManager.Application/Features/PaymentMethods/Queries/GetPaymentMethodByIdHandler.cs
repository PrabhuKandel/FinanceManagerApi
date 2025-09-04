using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Mapping;
using FinanceManager.Infrastructure.Data;
using MediatR;

namespace FinanceManager.Application.Features.PaymentMethods.Queries
{
    public class GetPaymentMethodByIdHandler(ApplicationDbContext _context) : IRequestHandler<GetPaymentMethodByIdQuery, OperationResult<PaymentMethodResponseDto>>
    {


        public async Task<OperationResult<PaymentMethodResponseDto>> Handle(GetPaymentMethodByIdQuery request, CancellationToken cancellationToken)
        {
            var paymentMethod = await _context.PaymentMethods.FindAsync(request.Id);
            if (paymentMethod == null)
            {
                throw new NotFoundException("Payment method not found");
            }

           
            return new OperationResult<PaymentMethodResponseDto>
            {

                Data = paymentMethod.ToResponseDto(),
                Message = "Payment Method retrieved successfully"


            };
        }
    }
}
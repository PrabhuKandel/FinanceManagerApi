using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.FeaturesStoredProcedure.PaymentMethods.Queries.GetPaymentMethodById
{
    public class GetPaymentMethodByIdSpHandler(IApplicationDbContext _context) : IRequestHandler<GetPaymentMethodByIdSpQuery, OperationResult<PaymentMethodResponseDto>>
    {
        public async Task<OperationResult<PaymentMethodResponseDto>> Handle(GetPaymentMethodByIdSpQuery request, CancellationToken cancellationToken)
        {
            var paymentMethod = _context.PaymentMethods
                            .FromSqlInterpolated($"EXECUTE dbo.usp_GetPaymentMethodById  @Id = {request.Id}")
                            .AsNoTracking()
                            .AsEnumerable()
                            .FirstOrDefault();

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

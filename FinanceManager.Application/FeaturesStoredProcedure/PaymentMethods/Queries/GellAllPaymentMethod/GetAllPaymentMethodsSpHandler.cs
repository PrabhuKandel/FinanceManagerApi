using System.Data;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Mapping;
using MediatR;
using Dapper;
using Microsoft.EntityFrameworkCore;
using FinanceManager.Application.Features.PaymentMethods.Mapping;


namespace FinanceManager.Application.FeaturesStoredProcedure.PaymentMethods.Queries.GellAllPaymentMethod
{
    public class GetAllPaymentMethodsSpHandler(IApplicationDbContext context  ) : IRequestHandler<GetAllPaymentMethodsSpQuery, OperationResult<IEnumerable<PaymentMethodResponseDto>>>
    {
        public async Task<OperationResult<IEnumerable<PaymentMethodResponseDto>>> Handle(GetAllPaymentMethodsSpQuery request, CancellationToken cancellationToken)
        {
            var paymentMethods = await context.PaymentMethods.FromSqlRaw("EXECUTE dbo.usp_GetAllPaymentMethods").ToListAsync(cancellationToken);

            //var paymentMethods = await connection.QueryAsync<PaymentMethod>("usp_GetAllPaymentMethods",commandType:CommandType.StoredProcedure);

            var paymentMethodDtos = paymentMethods.ToResponseDtoList();
            return new OperationResult<IEnumerable<PaymentMethodResponseDto>>
            {


                Data = paymentMethodDtos,
                Message = "Payment methods retrieved successfully"


            };

        }
    }

}

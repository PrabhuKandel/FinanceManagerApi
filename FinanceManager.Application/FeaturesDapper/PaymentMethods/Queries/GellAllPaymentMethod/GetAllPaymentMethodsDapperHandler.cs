
using System.Data;
using Dapper;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Interfaces;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.PaymentMethods.Queries.GellAllPaymentMethod
{

    public class GetAllPaymentMethodsDapperHandler(IDbConnection connection) : IRequestHandler<GetAllPaymentMethodsDapperQuery, OperationResult<IEnumerable<PaymentMethodResponseDto>>>
    {
       public async Task<OperationResult<IEnumerable<PaymentMethodResponseDto>>> Handle(GetAllPaymentMethodsDapperQuery request, CancellationToken cancellationToken)
       {
               
         var paymentMethods = await connection.QueryAsync<PaymentMethodResponseDto>("usp_GetAllPaymentMethods",commandType:CommandType.StoredProcedure);


         return new OperationResult<IEnumerable<PaymentMethodResponseDto>>
         {

            Data = paymentMethods,
            Message = "Payment methods retrieved successfully"


         };

       }
    }
    
}

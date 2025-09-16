using System.Data;
using Ardalis.GuardClauses;
using Dapper;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using MediatR;


namespace FinanceManager.Application.FeaturesDapper.PaymentMethods.Queries.GetPaymentMethodById
{
    public class GetPaymentMethodByIdDapperHandler(IDbConnection connection) : IRequestHandler<GetPaymentMethodByIdDapperQuery, OperationResult<PaymentMethodResponseDto>>
    {
        public async Task<OperationResult<PaymentMethodResponseDto>> Handle(GetPaymentMethodByIdDapperQuery request, CancellationToken cancellationToken)
        {

            var paymentMethod = await connection.QuerySingleOrDefaultAsync<PaymentMethodResponseDto>("usp_GetPaymentMethodById", new { request.Id }, commandType: CommandType.StoredProcedure);

            Guard.Against.Null(paymentMethod, nameof(paymentMethod), "Payment Method not found");

            return new OperationResult<PaymentMethodResponseDto>
            {

                Data = paymentMethod,
                Message = "Payment Method retrieved successfully"


            };
        }
    }
}

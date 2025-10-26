

using Dapper;
using System.Data;
using FinanceManager.Application.Common;
using FinanceManager.Application.FeaturesDapper.Reports.Dtos;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.Reports.Queries.TransactionRecordSummaryByPaymentMethod
{
    public class TransactionRecordSummaryByPaymentMethodHandler(IDbConnection connection) : IRequestHandler<TransactionRecordSummaryByPaymentMethodQuery, OperationResult<IEnumerable<TransactionRecordSummaryByPaymentMethodDto>>>
    {
        public async Task<OperationResult<IEnumerable<TransactionRecordSummaryByPaymentMethodDto>>> Handle(TransactionRecordSummaryByPaymentMethodQuery request, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PaymentMethodId", request.PaymentMethodId);
            parameters.Add("@FromDate", request.FromDate);
            parameters.Add("@ToDate", request.ToDate);


            var rows = await connection.QueryAsync<TransactionRecordSummaryByPaymentMethodDto>("usp_GetTransactionRecordSummaryByPaymentMethod", parameters, commandType: CommandType.StoredProcedure);

            return new OperationResult<IEnumerable<TransactionRecordSummaryByPaymentMethodDto>>
            {
                Data = rows,
                Message = "Transaction record summary retrieved successfully"
            };

        }
    }
}

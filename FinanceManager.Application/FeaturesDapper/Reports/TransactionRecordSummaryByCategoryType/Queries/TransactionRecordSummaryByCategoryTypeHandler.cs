
using System.Data;
using Dapper;
using FinanceManager.Application.FeaturesDapper.Reports.Dtos;
using FinanceManager.Application.Common;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.Reports.TransactionRecordSummaryByCategoryType.Queries
{
    public class TransactionRecordSummaryByCategoryTypeHandler(IDbConnection connection) : IRequestHandler<TransactionRecordSummaryByCategoryTypeQuery, OperationResult<IEnumerable<TransactionRecordSummaryByCategoryTypeDto>>>
    {
        public  async Task<OperationResult<IEnumerable<TransactionRecordSummaryByCategoryTypeDto>>> Handle(TransactionRecordSummaryByCategoryTypeQuery request, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@FromDate", request.FromDate);
            parameters.Add("@ToDate", request.ToDate);
            parameters.Add("@GroupBy", request.GroupBy.ToString().ToLower());


            var rows =  await connection.QueryAsync<TransactionRecordSummaryByCategoryTypeDto>("usp_GetTransactionRecordSummaryByCategoryType", parameters, commandType: CommandType.StoredProcedure);

            return new OperationResult<IEnumerable<TransactionRecordSummaryByCategoryTypeDto>>
            {
                Data = rows,
                Message = "Transaction record summary retrieved successfully"
            };
        }
    }
}

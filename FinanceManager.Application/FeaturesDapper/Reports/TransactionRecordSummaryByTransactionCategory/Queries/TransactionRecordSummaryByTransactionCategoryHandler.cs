using System.Data;
using Dapper;
using FinanceManager.Application.Common;
using FinanceManager.Application.FeaturesDapper.Reports.Dtos;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.Reports.TransactionRecordSummaryByTransactionCategory.Queries
{
        public class TransactionRecordSummaryByTransactionCategoryHandler(IDbConnection connection) : IRequestHandler<TransactionRecordSummaryByTransactionCategoryQuery, OperationResult<IEnumerable<TransactionRecordSummaryByCategoryDto>>>
        {
            public async Task<OperationResult<IEnumerable<TransactionRecordSummaryByCategoryDto>>> Handle(TransactionRecordSummaryByTransactionCategoryQuery request, CancellationToken cancellationToken)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TransactionCategoryId", request.TransactionCategoryId);
                parameters.Add("@FromDate", request.FromDate);
                parameters.Add("@ToDate", request.ToDate);


                var rows = await connection.QueryAsync<TransactionRecordSummaryByCategoryDto>("usp_GetTransactionRecordSummaryByTransactionCategory", parameters, commandType: CommandType.StoredProcedure);

                return new OperationResult<IEnumerable<TransactionRecordSummaryByCategoryDto>>
                {
                    Data = rows,
                    Message = "Transaction record summary retrieved successfully"
                };




            }
        }
    }


using System.Data;
using Dapper;
using FinanceManager.Application.Common;
using FinanceManager.Application.FeaturesDapper.Reports.Dtos;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.Reports.Queries.TransactionCategoryBudgetVsActualOutflow
{
    public class TransactionCategoryBudgetVsActualOutflowHandler(IDbConnection connection) : IRequestHandler<TransactionCategoryBudgetVsActualOutflowQuery, OperationResult<IEnumerable<TransactionCategoryBudgetVsActualOutflowDto>>>
    {
        public async Task<OperationResult<IEnumerable<TransactionCategoryBudgetVsActualOutflowDto>>> Handle(TransactionCategoryBudgetVsActualOutflowQuery request, CancellationToken cancellationToken)
        {
            var (periodStart, _) = PeriodCalculator.GetPeriodDates(request.PeriodType, request.PeriodStart);
            var (_, periodEnd) = PeriodCalculator.GetPeriodDates(request.PeriodType, request.PeriodEnd);

            var parameters = new DynamicParameters();
            parameters.Add("@TransactionCategoryId", request.TransactionCategoryId);
            parameters.Add("@PeriodType", request.PeriodType);
            parameters.Add("@PeriodStart", periodStart);
            parameters.Add("@PeriodEnd", periodEnd);

            var rows = await connection.QueryAsync<TransactionCategoryBudgetVsActualOutflowDto>(
                "usp_GetTransactionCategoryBudgetVsActualOutflow",
                parameters,
                commandType: CommandType.StoredProcedure);

            return new OperationResult<IEnumerable<TransactionCategoryBudgetVsActualOutflowDto>>
            {
                Message = "Report Generated ",
                Data = rows
            };




        }
    }
}

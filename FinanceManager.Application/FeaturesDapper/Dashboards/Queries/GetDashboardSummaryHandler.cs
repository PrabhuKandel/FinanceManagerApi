using System.Data;
using FinanceManager.Application.FeaturesDapper.Dashboards.Dtos;
using MediatR;
using Dapper;
using FinanceManager.Application.Common;
using FinanceManager.Application.Interfaces.Services;

namespace FinanceManager.Application.FeaturesDapper.Dashboards.Queries
{
    public class GetDashboardSummaryHandler(IDbConnection connection,IUserContext userContext) : IRequestHandler<GetDashboardSummaryQuery, OperationResult<DashboardSummaryResponseDto>>
    {
        public async Task<OperationResult<DashboardSummaryResponseDto>> Handle(GetDashboardSummaryQuery request, CancellationToken cancellationToken)
        {

            var applicationUserId = userContext.IsAdmin() ? null: userContext.UserId;

         var command  = new CommandDefinition
                (
                "usp_GetDashboardSummary",
                 new { ApplicationUserId = applicationUserId },
                commandType: CommandType.StoredProcedure,
                cancellationToken: cancellationToken
                );

            var dashbordSummary = await connection.QuerySingleAsync<DashboardSummaryResponseDto>(command);

            return new OperationResult<DashboardSummaryResponseDto>
            {
                Data = dashbordSummary,
         
                Message = "Dashboard summary retrieved successfully"


            };
        }
    }
}

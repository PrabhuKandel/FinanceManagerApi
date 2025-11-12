

using FinanceManager.Application.Common;
using FinanceManager.Application.FeaturesDapper.Dashboards.Dtos;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.Dashboards.Queries
{
    public record GetDashboardSummaryQuery : IRequest<OperationResult<DashboardSummaryResponseDto>>
    {
    }
}

using FinanceManager.Application.Common;
using FinanceManager.Application.FeaturesDapper.Reports.Dtos;
using FinanceManager.Domain.Enums;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.Reports.Queries.TransactionCategoryBudgetVsActualOutflow
{
    public record TransactionCategoryBudgetVsActualOutflowQuery
        (
            Guid? TransactionCategoryId,
            PeriodType PeriodType,
            String PeriodStart,
            String PeriodEnd
        ):IRequest<OperationResult<IEnumerable<TransactionCategoryBudgetVsActualOutflowDto>>> 
    {
    }
}

using FinanceManager.Application.Common;
using FinanceManager.Application.FeaturesDapper.Reports.Dtos;
using FinanceManager.Domain.Enums;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.Reports.TransactionCategoryBudgetVsActualOutflow.Queries
{
    public record TransactionCategoryBudgetVsActualOutflowQuery
        (
            Guid? TransactionCategoryId,
            PeriodType PeriodType,
            string PeriodStart,
            string PeriodEnd
        ):IRequest<OperationResult<IEnumerable<TransactionCategoryBudgetVsActualOutflowDto>>> 
    {
    }
}

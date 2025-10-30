
using FinanceManager.Domain.Enums;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.Reports.Queries.TransactionCategoryBudgetVsActualOutflow.ExportToPdf
{
    public record ExportTransactionCategoryBudgetVsActualOutflowQuery
        (
             Guid? TransactionCategoryId,
            PeriodType PeriodType,
            String PeriodStart,
            String PeriodEnd

        ) : IRequest<byte[]>
    {
    }
}

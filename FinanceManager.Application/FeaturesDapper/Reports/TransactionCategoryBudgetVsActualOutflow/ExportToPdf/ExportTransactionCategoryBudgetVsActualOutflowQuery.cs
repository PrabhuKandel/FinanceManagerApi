using FinanceManager.Domain.Enums;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.Reports.TransactionCategoryBudgetVsActualOutflow.ExportToPdf
{
    public record ExportTransactionCategoryBudgetVsActualOutflowQuery
        (
             Guid? TransactionCategoryId,
            PeriodType PeriodType,
            string PeriodStart,
            string PeriodEnd

        ) : IRequest<byte[]>
    {
    }
}

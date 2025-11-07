

using FinanceManager.Domain.Enums;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.Reports.Queries.TransactionCategoryBudgetVsActualOutflow.ExportToExcel
{
    public record ExportExcelTransactionCategoryBudgetVsActualOutflowQuery
    (
        Guid? TransactionCategoryId,
         PeriodType PeriodType,
         String PeriodStart,
         String PeriodEnd
    ): IRequest<byte[]>
    {
    }
}

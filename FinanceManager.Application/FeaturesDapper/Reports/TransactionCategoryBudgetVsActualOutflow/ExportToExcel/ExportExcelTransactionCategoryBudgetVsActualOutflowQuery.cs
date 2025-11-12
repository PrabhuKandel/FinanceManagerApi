using FinanceManager.Domain.Enums;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.Reports.TransactionCategoryBudgetVsActualOutflow.ExportToExcel
{
    public record ExportExcelTransactionCategoryBudgetVsActualOutflowQuery
    (
        Guid? TransactionCategoryId,
         PeriodType PeriodType,
         string PeriodStart,
         string PeriodEnd
    ): IRequest<byte[]>
    {
    }
}

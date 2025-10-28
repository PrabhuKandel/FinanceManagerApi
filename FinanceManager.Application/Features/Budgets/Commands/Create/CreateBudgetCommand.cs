using FinanceManager.Application.Common;
using FinanceManager.Domain.Enums;
using MediatR;

namespace FinanceManager.Application.Features.Budgets.Commands.Create
{
    public record CreateBudgetCommand
    (
        Guid TransactionCategoryId,
        decimal Amount,
        PeriodType PeriodType,
        string SelectedPeriod,
        bool IsActive
    ) : IRequest<OperationResult<Guid>>
    {
    }
}



using FinanceManager.Application.Common;
using FinanceManager.Application.Features.Budgets.Dtos;
using FinanceManager.Domain.Enums;
using MediatR;

namespace FinanceManager.Application.Features.Budgets.Queries.GetAll
{
    public record GetAllBudgetsQuery(PeriodType PeriodType):IRequest<OperationResult<IEnumerable<BudgetResponseDto>>>
    {
    }
}

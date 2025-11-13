

using FinanceManager.Application.Common;
using FinanceManager.Application.Features.Budgets.Dtos;
using MediatR;

namespace FinanceManager.Application.Features.Budgets.Queries.GetAll
{
    public record GetAllBudgetsQuery:IRequest<OperationResult<IEnumerable<BudgetResponseDto>>>
    {
    }
}

using FinanceManager.Application.Common;
using FinanceManager.Application.Features.Budgets.Dtos;
using FinanceManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.Budgets.Queries.GetAll
{
    public class GetAllBudgetsHandler(IApplicationDbContext context) : IRequestHandler<GetAllBudgetsQuery, OperationResult<IEnumerable<BudgetResponseDto>>>
    {
        public async Task<OperationResult<IEnumerable<BudgetResponseDto>>> Handle(GetAllBudgetsQuery request, CancellationToken cancellationToken)
        {
            var budgets = await context.Budgets
                  .Include(b => b.TransactionCategory)
                  .Where(b => b.IsActive && b.PeriodType==request.PeriodType)
                  .OrderBy(b => b.PeriodType)
                  .ThenByDescending(b => b.PeriodEnd)      
                  .Select(b => new BudgetResponseDto
                  {
                      Id = b.Id,
                      TransactionCategoryName = b.TransactionCategory!.Name,
                      Amount = b.Amount,
                      PeriodType = b.PeriodType.ToString(),
                      PeriodStart = b.PeriodStart,
                      PeriodEnd = b.PeriodEnd
                  })
                  .ToListAsync(cancellationToken);


            return new OperationResult<IEnumerable<BudgetResponseDto>>
            {
                Data = budgets,
                Message = "Budgets retrieved successfully",
            };
        }
    }
}

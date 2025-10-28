

using FinanceManager.Application.Features.Budgets.Commands.Create;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Services;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FinanceManager.Application.Features.Budgets.Mapping
{
    public static  class BudgetMappingProfile
    {
        public static  Budget ToEntity(this CreateBudgetCommand command , DateTime periodStart, DateTime periodEnd , IUserContext userContext)
        {
           return new Budget
            {
                TransactionCategoryId = command.TransactionCategoryId,
                Amount = command.Amount,
                PeriodStart = periodStart,
                PeriodEnd = periodEnd,
                CreatedByApplicationUserId = userContext.UserId,
                CreatedAt = DateTime.UtcNow,
                Status = userContext.IsAdmin() ? BudgetApprovalStatus.Approved : BudgetApprovalStatus.Pending,
                IsActive = command.IsActive



            };
        }
    }
}

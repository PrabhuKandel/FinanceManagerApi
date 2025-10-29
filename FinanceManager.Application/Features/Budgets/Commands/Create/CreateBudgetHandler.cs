using FinanceManager.Application.Common;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Features.Budgets.Mapping;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Interfaces.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.Budgets.Commands.Create
{
    public class CreateBudgetHandler(IApplicationDbContext context,IUserContext userContext) : IRequestHandler<CreateBudgetCommand, OperationResult<Guid>>
    {
        public async Task<OperationResult<Guid>> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)


        {
            // Calculate PeriodStart and PeriodEnd using helper
            var (periodStart, periodEnd) = PeriodCalculator.GetPeriodDates(request.PeriodType, request.SelectedPeriod);


            bool exists = await context.Budgets.AnyAsync(b =>
                            b.TransactionCategoryId == request.TransactionCategoryId &&
                            b.PeriodStart == periodStart &&
                            b.PeriodEnd == periodEnd &&
                            b.IsActive, cancellationToken);

            if (exists)
                throw new BusinessValidationException( nameof(request.TransactionCategoryId), new[] { "A budget already exists for this category and period." });

            var budget = request.ToEntity(periodStart, periodEnd, userContext);
            
            context.Budgets.Add(budget);
            await context.SaveChangesAsync(cancellationToken);

            return new OperationResult<Guid>
            {
                Data = budget.Id,
                Message = "Budget Created Successfully"
            };
        }
    }
}

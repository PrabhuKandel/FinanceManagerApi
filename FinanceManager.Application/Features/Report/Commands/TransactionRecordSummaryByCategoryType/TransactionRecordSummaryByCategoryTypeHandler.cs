

using Azure;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.Report;
using FinanceManager.Application.Interfaces;
using FinanceManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.Report.Commands.TransactionRecordSummaryByIncomeExpense
{
    public class TransactionRecordSummaryByCategoryTypeHandler(IApplicationDbContext context) : IRequestHandler<TransactionRecordSummaryByCategoryTypeCommand, OperationResult<TransactionRecordSummaryByCategoryTypeDto>>
    {
        public async Task<OperationResult<TransactionRecordSummaryByCategoryTypeDto>> Handle(TransactionRecordSummaryByCategoryTypeCommand request, CancellationToken cancellationToken)
        {
            var query = context.TransactionRecords
          .Include(tr => tr.TransactionCategory)
          .AsQueryable();

            // Apply optional filters
            if (request.From.HasValue)
                query = query.Where(tr => tr.TransactionDate >= request.From.Value);

            if (request.To.HasValue)
                query = query.Where(tr => tr.TransactionDate <= request.To.Value);


            var response = new TransactionRecordSummaryByCategoryTypeDto();

            if (request.CategoryType == CategoryType.Income)
            {
                // Only income requested
                response.TotalIncome = await query
                    .Where(tr => tr.TransactionCategory!.Type == CategoryType.Income)
                    .SumAsync(tr => (decimal?)tr.Amount, cancellationToken) ?? 0m;
            }
            else if (request.CategoryType == CategoryType.Expense)
            {
                // Only expense requested
                 response.TotalExpense = await query
                    .Where(tr => tr.TransactionCategory!.Type == CategoryType.Expense)
                    .SumAsync(tr => (decimal?)tr.Amount, cancellationToken) ?? 0m;
            }
            else
            {
                // No category filter → calculate both
                response.TotalIncome = await query
                    .Where(tr => tr.TransactionCategory!.Type == CategoryType.Income)
                    .SumAsync(tr => (decimal?)tr.Amount, cancellationToken) ?? 0m;

                response.TotalExpense = await query
                    .Where(tr => tr.TransactionCategory!.Type == CategoryType.Expense)
                    .SumAsync(tr => (decimal?)tr.Amount, cancellationToken) ?? 0m;
            }

            return new OperationResult<TransactionRecordSummaryByCategoryTypeDto>
            {
                Message = " Report Generated Successfully",
                Data = response,
            };





        }
    }
}

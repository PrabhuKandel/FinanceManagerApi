

using Azure;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.Report;
using FinanceManager.Application.Interfaces;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Enums;
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
          .Where(tr=>tr.ApprovalStatus==TransactionRecordApprovalStatus.Approved)
          .AsQueryable();

            // Apply optional filters
            if (request.From.HasValue)
                query = query.Where(tr => tr.TransactionDate >= request.From.Value);

            if (request.To.HasValue)
                query = query.Where(tr => tr.TransactionDate <= request.To.Value);


            // Single database query for optimal performance
            var summaryData = await query
                .GroupBy(tr => tr.TransactionCategory!.Type)
                .Select(g => new
                {
                    CategoryType = g.Key,
                    TotalAmount = g.Sum(tr => tr.Amount)
                })
                .ToListAsync(cancellationToken);

            var response = new TransactionRecordSummaryByCategoryTypeDto();

            // Process results in memory (much faster)
            if (request.CategoryType == CategoryType.Income)
            {
                response.TotalIncome = summaryData
                .FirstOrDefault(x => x.CategoryType == CategoryType.Income)?.TotalAmount ?? 0m;
            }
            else if (request.CategoryType == CategoryType.Expense)
            {
                response.TotalExpense = summaryData
                .FirstOrDefault(x => x.CategoryType == CategoryType.Expense)?.TotalAmount ?? 0m;
            }
            else
            {
                // No filter - get both
                response.TotalIncome = summaryData
                .FirstOrDefault(x => x.CategoryType == CategoryType.Income)?.TotalAmount ?? 0m;

                response.TotalExpense = summaryData
                .FirstOrDefault(x => x.CategoryType == CategoryType.Expense)?.TotalAmount ?? 0m;
            }

            return new OperationResult<TransactionRecordSummaryByCategoryTypeDto>
            {
                Message = " Report Generated Successfully",
                Data = response,
            };





        }
    }
}

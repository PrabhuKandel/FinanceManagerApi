
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.Report;
using FinanceManager.Domain.Entities;
using MediatR;

namespace FinanceManager.Application.Features.Report.Commands.TransactionRecordSummaryByIncomeExpense
{
    public record TransactionRecordSummaryByCategoryTypeCommand
        (
            DateTime? From,
            DateTime? To,
           CategoryType? CategoryType
        ) : IRequest<OperationResult<TransactionRecordSummaryByCategoryTypeDto>>
    {
    }
}

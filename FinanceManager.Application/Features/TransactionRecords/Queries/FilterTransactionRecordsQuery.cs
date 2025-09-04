using FinanceManager.Application.Dtos.TransactionRecord;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Queries
{
    public record FilterTransactionRecordsQuery(decimal? minAmount, decimal? maxAmount, Guid? transactionCategory, Guid? paymentMethod, DateTime? transactionDate):IRequest<IEnumerable<TransactionRecordResponseDto>>
    {
    }
}

using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Queries
{
    public record FilterTransactionRecordsQuery(decimal? minAmount, decimal? maxAmount, Guid? transactionCategoryId, Guid? paymentMethodId, DateTime? transactionDate):IRequest<OperationResult<IEnumerable<TransactionRecordResponseDto>>>
    {
    }
}

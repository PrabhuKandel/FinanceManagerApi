using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.TransactionRecords.Commands.CreateTransactionRecord
{
    public record CreateTransactionRecordDapperCommand(Guid TransactionCategoryId, Guid PaymentMethodId, Decimal Amount, string? Description, DateTime TransactionDate) : IRequest<OperationResult<TransactionRecordResponseDto>>
    {
    }
}

using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.TransactionRecords.Commands.UpdateTransactionRecord
{
    public record UpdateTransactionRecordDapperCommand(Guid Id, Guid TransactionCategoryId, Guid PaymentMethodId, Decimal Amount, string? Description, DateTime TransactionDate) : IRequest<OperationResult<TransactionRecordResponseDto>>
    {
    }
}

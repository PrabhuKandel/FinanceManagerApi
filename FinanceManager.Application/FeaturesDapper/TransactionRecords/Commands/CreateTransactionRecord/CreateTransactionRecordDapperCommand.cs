using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionPayment;
using FinanceManager.Application.Dtos.TransactionRecord;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.TransactionRecords.Commands.CreateTransactionRecord
{
    public record CreateTransactionRecordDapperCommand(
        Guid TransactionCategoryId,
        Decimal Amount, 
        string? Description,
        DateTime TransactionDate,
        List<TransactionPaymentDto> Payments
        ) : IRequest<OperationResult<TransactionRecordResponseDto>>
    {
    }
}

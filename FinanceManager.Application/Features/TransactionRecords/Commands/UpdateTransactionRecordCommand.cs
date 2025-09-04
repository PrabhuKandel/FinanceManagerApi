using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Commands
{
    public record UpdateTransactionRecordCommand(Guid Id, TransactionRecordUpdateDto transactionRecord) : IRequest<OperationResult<TransactionRecordResponseDto>>
    {
    }
}

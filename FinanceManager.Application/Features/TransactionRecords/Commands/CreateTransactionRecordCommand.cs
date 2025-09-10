using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Commands
{
    public record CreateTransactionRecordCommand(TransactionRecordCreateDto TransactionRecord):IRequest<OperationResult<TransactionRecordResponseDto>>
    {
    }
}

using FinanceManager.Application.Dtos.TransactionRecord;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Commands
{
    public record CreateTransactionRecordCommand(TransactionRecordCreateDto transactionRecord):IRequest<TransactionRecordResponseDto>
    {
    }
}

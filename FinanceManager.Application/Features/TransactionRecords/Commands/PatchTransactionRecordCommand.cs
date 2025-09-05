using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Commands
{
    public  record PatchTransactionRecordCommand(Guid Id, TransactionRecordPatchDto TransactionRecord) : IRequest<OperationResult<TransactionRecordResponseDto>>
    {
    }
}

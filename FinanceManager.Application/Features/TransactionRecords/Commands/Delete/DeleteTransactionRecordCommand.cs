using FinanceManager.Application.Common;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Commands.Delete
{
    public record DeleteTransactionRecordCommand(Guid Id) : IRequest<OperationResult<string>>
    {

    }
}

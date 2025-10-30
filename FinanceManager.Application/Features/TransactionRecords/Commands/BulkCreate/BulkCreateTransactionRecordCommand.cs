

using FinanceManager.Application.Common;
using FinanceManager.Application.Features.TransactionRecords.Dtos;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Commands.BulkCreate
{
    public record BulkCreateTransactionRecordCommand(List<BulkCreateTransactionRecordDto> TransactionRecords) : IRequest<OperationResult<string>>
    {

    }
}



using FinanceManager.Application.Common;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Commands.DeleteAttachment
{
    public record DeleteTransactionRecordAttachmentCommand
        (
        Guid TransactionRecordId,
        List<Guid> TransactionAttachmentIds
        ) : IRequest<OperationResult<string>>
    {
    }
}

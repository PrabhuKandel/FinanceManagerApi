

using FinanceManager.Application.Common;
using FinanceManager.Application.Interfaces.Services;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Commands.DeleteAttachment
{
    public class DeleteTransactionRecordAttachmentHandler( ITransactionAttachmentService transactionAttachmentService):IRequestHandler<DeleteTransactionRecordAttachmentCommand, OperationResult<string>>
    {
        public async Task<OperationResult<string>> Handle(DeleteTransactionRecordAttachmentCommand request, CancellationToken cancellationToken)
        {
            await transactionAttachmentService.DeleteAttachmentsAsync(
               request.TransactionRecordId,
               request.TransactionAttachmentIds);

            return new OperationResult<string>
            {
                Message = "Attachments deleted successfully"
            };
        }
    }
  
}

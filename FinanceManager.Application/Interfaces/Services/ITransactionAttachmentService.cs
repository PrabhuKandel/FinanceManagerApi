using Microsoft.AspNetCore.Http;

namespace FinanceManager.Application.Interfaces.Services
{
    public interface ITransactionAttachmentService
    {
        Task SaveAttachmentsAsync(Guid transactionRecordId, IFormFile[] files, string uploadedByApplicationUserId);
        Task DeleteAttachmentsAsync(Guid transactionRecordId, List<Guid> attachmentIds);
    }
}

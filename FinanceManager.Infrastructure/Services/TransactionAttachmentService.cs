
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Domain.Entities;
using FinanceManager.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Infrastructure.Services
{
    public class TransactionAttachmentService : ITransactionAttachmentService
    {
          private readonly ApplicationDbContext _context;
        private readonly string _uploadFolder;

        public TransactionAttachmentService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "TransactionRecords");

            if (!Directory.Exists(_uploadFolder))
                Directory.CreateDirectory(_uploadFolder);
        }

        public async Task SaveAttachmentsAsync(Guid transactionRecordId, IFormFile[] files, string uploadedByApplicationUserId)
        {

            foreach (var file in files)
            {
                var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
                var filePath = Path.Combine(_uploadFolder, uniqueFileName);

                // Save file to disk
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Save metadata to DB
                var attachment = new TransactionAttachment
                {
                    TransactionRecordId = transactionRecordId,
                    FileName = file.FileName,
                    FilePath = filePath,

                    FileType = file.ContentType,
                    UploadedByApplicationUserId = uploadedByApplicationUserId,
                    UploadDate = DateTime.UtcNow
                };

                await _context.TransactionAttachments.AddAsync(attachment);
            }

          
        }

        public async Task DeleteAttachmentsAsync(Guid transactionRecordId, List<Guid> attachmentIds)
        {

            var attachments = await _context.TransactionAttachments
                .Where(a => attachmentIds.Contains(a.Id) && a.TransactionRecordId == transactionRecordId)
                .ToListAsync();

            foreach (var attachment in attachments)
            {
                if (File.Exists(attachment.FilePath))
                    File.Delete(attachment.FilePath);

                _context.TransactionAttachments.Remove(attachment);
            }

            await _context.SaveChangesAsync();
        }
    }
}

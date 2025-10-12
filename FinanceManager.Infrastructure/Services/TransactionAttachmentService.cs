
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Domain.Entities;
using FinanceManager.Infrastructure.Data;
using Microsoft.AspNetCore.Http;

namespace FinanceManager.Infrastructure.Services
{
    internal class TransactionAttachmentService : ITransactionAttachmentService
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
    }
}

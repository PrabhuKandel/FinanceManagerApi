using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FinanceManager.Application.Interfaces.Services
{
    public interface ITransactionAttachmentService
    {
        Task SaveAttachmentsAsync(Guid transactionRecordId, IFormFile[] files, string uploadedByApplicationUserId);
    }
}

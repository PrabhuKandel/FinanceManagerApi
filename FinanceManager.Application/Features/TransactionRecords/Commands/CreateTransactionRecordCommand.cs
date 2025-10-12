using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Application.Features.TransactionRecords.Commands
{
    public record CreateTransactionRecordCommand(TransactionRecordCreateDto TransactionRecord, IFormFile[]? TransactionAttachments ):IRequest<OperationResult<TransactionRecordResponseDto>>
    {
    }
}

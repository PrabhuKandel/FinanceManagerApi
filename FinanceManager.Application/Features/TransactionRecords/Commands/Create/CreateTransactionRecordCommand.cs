using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.Shared;
using FinanceManager.Application.Features.TransactionRecords.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace FinanceManager.Application.Features.TransactionRecords.Commands.Create
{
    public record CreateTransactionRecordCommand(
            Guid TransactionCategoryId,
            decimal Amount,
            string? Description,
            DateTime TransactionDate,
            List<TransactionPaymentDto> Payments,
            IFormFile[]? TransactionAttachments


        ) :IRequest<OperationResult<TransactionRecordResponseDto>>
    {
 
    }
}

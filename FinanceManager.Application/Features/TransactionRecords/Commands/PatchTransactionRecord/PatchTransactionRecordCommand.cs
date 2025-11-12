using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.Shared;
using FinanceManager.Application.Features.TransactionRecords.Dtos;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Commands.PatchTransactionRecord
{
    public  record PatchTransactionRecordCommand(
        Guid Id,
        Guid? TransactionCategoryId,
        decimal? Amount,
        string? Description,
        DateTime? TransactionDate,
         List<TransactionPaymentDto>? Payments 

        ) : IRequest<OperationResult<TransactionRecordResponseDto>>
    {
    }
}

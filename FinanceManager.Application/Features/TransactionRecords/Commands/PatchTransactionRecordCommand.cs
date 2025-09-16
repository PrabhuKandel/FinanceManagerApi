using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Commands
{
    public  record PatchTransactionRecordCommand(
        Guid Id,
        Guid? TransactionCategoryId,
        Guid? PaymentMethodId,
        decimal? Amount,
        string? Description,
        DateTime? TransactionDate

        ) : IRequest<OperationResult<TransactionRecordResponseDto>>
    {
    }
}

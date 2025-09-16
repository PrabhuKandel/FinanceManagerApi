
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using MediatR;

namespace FinanceManager.Application.FeaturesDapper.TransactionRecords.Commands.PatchTransactionRecord
{
    public record PatchTransactionRecordDapperCommand(
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

using FinanceManager.Application.Common;
using FinanceManager.Domain.Enums;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Commands.PatchApprovalStatus
{
    public record PatchTransactionRecordApprovalStatusCommand
    (
    Guid Id,
    TransactionRecordApprovalStatus ApprovalStatus
    ) : IRequest<OperationResult<string>>
    {
    }
}
    
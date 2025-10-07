

using FinanceManager.Application.Common;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Interfaces.Services;
using MediatR;
using static Dapper.SqlMapper;

namespace FinanceManager.Application.Features.TransactionRecords.Commands
{
    public class PatchTransactionRecordApprovalStatusHandler(IApplicationDbContext context, IUserContext userContext) : IRequestHandler<PatchTransactionRecordApprovalStatusCommand, OperationResult<string>>
    {
        public Task<OperationResult<string>> Handle(PatchTransactionRecordApprovalStatusCommand request, CancellationToken cancellationToken)
        {
           var transactionRecordEntity = context.TransactionRecords
                .FirstOrDefault(tr => tr.Id == request.Id);

            // Only admins can change approval status
            if (!userContext.IsAdmin())
            {
                throw new AuthorizationException("You do not have permission to change the approval status.");
            }
            transactionRecordEntity!.ApprovalStatus = request.ApprovalStatus;

            transactionRecordEntity.ActionedAt = DateTime.UtcNow;            
            transactionRecordEntity.ActionedByApplicationUserId = userContext.UserId;

            transactionRecordEntity.UpdatedAt = DateTime.UtcNow;
            transactionRecordEntity.UpdatedByApplicationUserId = userContext.UserId;


            context.SaveChangesAsync(cancellationToken);
            return Task.FromResult(new OperationResult<string>
            {
                Message = " Approval status updated successfully.",

            });
        }
    }
}

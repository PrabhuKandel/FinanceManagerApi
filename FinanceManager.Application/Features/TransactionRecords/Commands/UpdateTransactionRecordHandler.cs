using Ardalis.GuardClauses;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.TransactionRecords.Commands
{
    public class UpdateTransactionRecordHandler : IRequestHandler<UpdateTransactionRecordCommand, OperationResult<TransactionRecordResponseDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IUserContext userContext;

        public UpdateTransactionRecordHandler(IApplicationDbContext _context, IUserContext _userContext)
        {
            context = _context;
            userContext = _userContext;
        }

        public async Task<OperationResult<TransactionRecordResponseDto>> Handle(UpdateTransactionRecordCommand request, CancellationToken cancellationToken)
        {

            var transactionRecordFromDb = await context.TransactionRecords.FindAsync(request.Id);
            Guard.Against.Null(transactionRecordFromDb, nameof(transactionRecordFromDb), "Transaction record not found");

            if (!userContext.IsAdmin())
            {
                if (transactionRecordFromDb?.CreatedByApplicationUserId != userContext.UserId)
                {
                    throw new AuthorizationException("You can't access this record.");
                }
            }

            transactionRecordFromDb.UpdatedByApplicationUserId = userContext.UserId;


            transactionRecordFromDb?.UpdateEntity(request.TransactionRecord);

            await context.SaveChangesAsync();

            var transactionRecord = await context.TransactionRecords
           .Include(t => t.TransactionCategory)
           .Include(t => t.PaymentMethod)
           .Include(t => t.CreatedByApplicationUser)
           .Include(t => t.UpdatedByApplicationUser)
           .FirstAsync(t => t.Id == request.Id, cancellationToken);

            return new OperationResult<TransactionRecordResponseDto>
            {

                Message = "Transaction category updated",
                Data = transactionRecord.ToResponseDto(userContext.IsAdmin())
            };

        }


    }
}

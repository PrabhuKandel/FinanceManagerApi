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
    public class PatchTransactionRecordHandler : IRequestHandler<PatchTransactionRecordCommand, OperationResult<TransactionRecordResponseDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IUserContext userContext;

        public PatchTransactionRecordHandler( IApplicationDbContext _context, IUserContext _userContext)
        {
            context = _context;
            userContext = _userContext;
        }

        public async Task<OperationResult<TransactionRecordResponseDto>> Handle(PatchTransactionRecordCommand request, CancellationToken cancellationToken)
        {
            var transactionRecordFromDb = await context.TransactionRecords.FindAsync(request.Id,cancellationToken);
           Guard.Against.Null(transactionRecordFromDb, nameof(transactionRecordFromDb), "Transaction record not found");

            // Authorization check
            if (!userContext.IsAdmin() && transactionRecordFromDb.CreatedByApplicationUserId != userContext.UserId)
            {
                
                   throw new AuthorizationException();
                
            }
             
            transactionRecordFromDb.TransactionCategoryId = request.TransactionCategoryId ?? transactionRecordFromDb.TransactionCategoryId;
            transactionRecordFromDb.PaymentMethodId = request.PaymentMethodId ?? transactionRecordFromDb.PaymentMethodId;
            transactionRecordFromDb.Amount = request.Amount ?? transactionRecordFromDb.Amount;
            transactionRecordFromDb.Description = request.Description ?? transactionRecordFromDb.Description;
            transactionRecordFromDb.TransactionDate = request.TransactionDate ?? transactionRecordFromDb.TransactionDate;
            transactionRecordFromDb.UpdatedByApplicationUserId = userContext.UserId;

            await context.SaveChangesAsync(cancellationToken);


              var transactionRecord = await context.TransactionRecords
             .Include(t => t.TransactionCategory)
             .Include(t => t.PaymentMethod)
             .Include(t => t.CreatedByApplicationUser)
             .Include(t => t.UpdatedByApplicationUser)
             .FirstAsync(t => t.Id == request.Id, cancellationToken);

            return new OperationResult<TransactionRecordResponseDto>
            {
                Message = "Transaction record patched",
                Data = transactionRecord.ToResponseDto(userContext.IsAdmin())
            };
        }



  
    }
}

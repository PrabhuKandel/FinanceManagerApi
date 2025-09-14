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
            if (transactionRecordFromDb == null)
            {
                throw new NotFoundException("Transaction record doesn't exist");
            }

            // Authorization check
            if (!userContext.IsAdmin() && transactionRecordFromDb.CreatedByApplicationUserId != userContext.UserId)
            {
                
                   throw new AuthorizationException();
                
            }

            var patchDto = request.TransactionRecord;

            // Update only provided properties
            if (patchDto.TransactionCategoryId.HasValue)
            {
                if (!await context.TransactionCategories
                    .AnyAsync(c => c.Id == patchDto.TransactionCategoryId.Value, cancellationToken))
                    throw new BusinessValidationException("Invalid Transaction Category");

                transactionRecordFromDb.TransactionCategoryId = patchDto.TransactionCategoryId.Value;
            }

            if (patchDto.PaymentMethodId.HasValue)
            {
                if (!await context.PaymentMethods
                    .AnyAsync(p => p.Id == patchDto.PaymentMethodId.Value, cancellationToken))
                    throw new BusinessValidationException("Invalid Payment Method");

                transactionRecordFromDb.PaymentMethodId = patchDto.PaymentMethodId.Value;
            }

            if (patchDto.Amount.HasValue)
                transactionRecordFromDb.Amount = patchDto.Amount.Value;

            if (!string.IsNullOrWhiteSpace(patchDto.Description))
                transactionRecordFromDb.Description = patchDto.Description;

            if (patchDto.TransactionDate.HasValue)
                transactionRecordFromDb.TransactionDate = patchDto.TransactionDate.Value;

           
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

using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Mapping;
using FinanceManager.Domain.Entities;
using FinanceManager.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.TransactionRecords.Commands
{
    public class PatchTransactionRecordHandler(UserManager<ApplicationUser> _userManager, ApplicationDbContext _context, IUserContext _userContext) : IRequestHandler<PatchTransactionRecordCommand, OperationResult<TransactionRecordResponseDto>>
    {
        public async Task<OperationResult<TransactionRecordResponseDto>> Handle(PatchTransactionRecordCommand request, CancellationToken cancellationToken)
        {
            var transactionRecordFromDb = await _context.TransactionRecords.FindAsync(request.Id);
            if (transactionRecordFromDb == null)
            {
                throw new NotFoundException("Transaction record doesn't exist");
            }

            // Authorization check
            if (!await IsUserAdmin(_userContext.UserId) && transactionRecordFromDb.CreatedByApplicationUserId != _userContext.UserId)
            {
                
                   throw new UnauthorizedAccessException("You can't access this record.");
                
            }

            var patchDto = request.TransactionRecord;

            // Update only provided properties
            if (patchDto.TransactionCategoryId.HasValue)
            {
                if (!await _context.TransactionCategories
                    .AnyAsync(c => c.Id == patchDto.TransactionCategoryId.Value, cancellationToken))
                    throw new CustomValidationException("Invalid Transaction Category");

                transactionRecordFromDb.TransactionCategoryId = patchDto.TransactionCategoryId.Value;
            }

            if (patchDto.PaymentMethodId.HasValue)
            {
                if (!await _context.PaymentMethods
                    .AnyAsync(p => p.Id == patchDto.PaymentMethodId.Value, cancellationToken))
                    throw new CustomValidationException("Invalid Payment Method");

                transactionRecordFromDb.PaymentMethodId = patchDto.PaymentMethodId.Value;
            }

            if (patchDto.Amount.HasValue)
                transactionRecordFromDb.Amount = patchDto.Amount.Value;

            if (!string.IsNullOrWhiteSpace(patchDto.Description))
                transactionRecordFromDb.Description = patchDto.Description;

            if (patchDto.TransactionDate.HasValue)
                transactionRecordFromDb.TransactionDate = patchDto.TransactionDate.Value;

           
            transactionRecordFromDb.UpdatedByApplicationUserId = _userContext.UserId;

              await _context.SaveChangesAsync(cancellationToken);


               var transactionRecord = await _context.TransactionRecords
             .Include(t => t.TransactionCategory)
             .Include(t => t.PaymentMethod)
             .Include(t => t.CreatedByApplicationUser)
             .Include(t => t.UpdatedByApplicationUser)
             .FirstAsync(t => t.Id == request.Id, cancellationToken);

            return new OperationResult<TransactionRecordResponseDto>
            {
                Message = "Transaction record patched",
                Data = transactionRecord.ToResponseDto(await IsUserAdmin(_userContext.UserId))
            };
        }



        private async Task<bool> IsUserAdmin(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;
            var roles = await _userManager.GetRolesAsync(user);
            return roles.Contains(RoleConstants.Admin);
        }
    }
}

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
    public class UpdateTransactionRecordHandler(ApplicationDbContext _context, IUserContext _userContext, UserManager<ApplicationUser> _userManager) : IRequestHandler<UpdateTransactionRecordCommand, OperationResult<TransactionRecordResponseDto>>
    {
        public async Task<OperationResult<TransactionRecordResponseDto>> Handle(UpdateTransactionRecordCommand request, CancellationToken cancellationToken)
        {

            var transactionRecordFromDb = await _context.TransactionRecords.FindAsync(request.Id);
            if (transactionRecordFromDb == null)
            {
                throw new NotFoundException("Transaction record doesn't exist");
            }

            if (!await IsUserAdmin(_userContext.UserId))
            {
                if (transactionRecordFromDb.CreatedByApplicationUserId != _userContext.UserId)
                {
                    throw new UnauthorizedAccessException("You can't access this record.");
                }
            }

            if (!await _context.TransactionCategories.AnyAsync(c => c.Id == request.transactionRecord.TransactionCategoryId))
                throw new CustomValidationException("Invalid Transaction Category");

            if (!await _context.TransactionCategories.AnyAsync(c => c.Id == request.transactionRecord.PaymentMethodId))
                throw new CustomValidationException("Invalid Payment Method");

            transactionRecordFromDb.UpdatedByApplicationUserId = _userContext.UserId;


            transactionRecordFromDb.UpdateEntity(request.transactionRecord);

            await _context.SaveChangesAsync();

            var transactionRecord = await _context.TransactionRecords
           .Include(t => t.TransactionCategory)
           .Include(t => t.PaymentMethod)
           .Include(t => t.CreatedByApplicationUser)
           .Include(t => t.UpdatedByApplicationUser)
           .FirstAsync(t => t.Id == request.Id, cancellationToken);

            return new OperationResult<TransactionRecordResponseDto>
            {

                Message = "Transaction category updated",
                Data = transactionRecord.ToResponseDto(await IsUserAdmin(_userContext.UserId))
            };

        }
        private async Task<bool> IsUserAdmin(String userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;
            var roles = await _userManager.GetRolesAsync(user);
            return roles.Contains(RoleConstants.Admin);
        }

    }
}

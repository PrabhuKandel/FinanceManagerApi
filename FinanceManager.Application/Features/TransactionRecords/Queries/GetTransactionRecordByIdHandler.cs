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

namespace FinanceManager.Application.Features.TransactionRecords.Queries
{
    public class GetTransactionRecordByIdHandler (ApplicationDbContext _context,IUserContext _userContext,UserManager<ApplicationUser> _userManager): IRequestHandler<GetTransactionRecordByIdQuery, OperationResult<TransactionRecordResponseDto>>
    {
        public async Task<OperationResult<TransactionRecordResponseDto>> Handle(GetTransactionRecordByIdQuery request, CancellationToken cancellationToken)
        {
            
            var transactionRecord = await _context.TransactionRecords
                .Include(tr => tr.TransactionCategory)
                .Include(tr => tr.PaymentMethod)
                .Include(t => t.CreatedByApplicationUser)
                .Include(t => t.UpdatedByApplicationUser).FirstOrDefaultAsync(tr => tr.Id == request.Id);


            if (transactionRecord == null)
            {
                throw new NotFoundException("Transaction record not found");
            }
            var isAdmin = await IsUserAdmin(_userContext.UserId);
            // Filter for non-admin users
            if (!isAdmin)
            {
                if (transactionRecord.CreatedByApplicationUserId != _userContext.UserId)
                {
                    throw new UnauthorizedAccessException("You can't access this record.");
                }
            }
                var transactionRecordDto =   transactionRecord.ToResponseDto(isAdmin);
             return new OperationResult<TransactionRecordResponseDto>
            {

                Data = transactionRecordDto,
                Message = "Transaction record  retrieved successfully"


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

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
    public class GetAllTransactionRecordsHandler(ApplicationDbContext _context,UserManager<ApplicationUser> _userManager,IUserContext _userContext) : IRequestHandler<GetAllTransactionRecordsQuery, OperationResult<IEnumerable<TransactionRecordResponseDto>>>
    {
        public async Task<OperationResult<IEnumerable<TransactionRecordResponseDto>>> Handle(GetAllTransactionRecordsQuery request, CancellationToken cancellationToken)
        {
            var transactionRecordsFromDb =  await  _context.TransactionRecords
                .Include(tr => tr.TransactionCategory)
                .Include(tr => tr.PaymentMethod).Include(t => t.CreatedByApplicationUser)
                                    .Include(t => t.UpdatedByApplicationUser).ToListAsync();
            if (!transactionRecordsFromDb.Any())
            {
                throw new NotFoundException("Transaction record doesn't exist");
            }
            // Check admin status once
            var isAdmin = await IsUserAdmin(_userContext.UserId);
            // Filter for non-admin users
            if (!isAdmin)
            {
                transactionRecordsFromDb = transactionRecordsFromDb
                    .Where(t => t.CreatedByApplicationUserId == _userContext.UserId)
                    .ToList();
            }

           var  transactionRecordsDtos =  transactionRecordsFromDb.ToResponseDtoList(isAdmin);
            return new OperationResult<IEnumerable<TransactionRecordResponseDto>>
            {


                Data = transactionRecordsDtos,
                Message = "Transaction records retrieved successfully"

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

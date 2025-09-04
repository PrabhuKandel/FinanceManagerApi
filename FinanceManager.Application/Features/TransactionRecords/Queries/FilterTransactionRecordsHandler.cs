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
    public class FilterTransactionRecordsHandler(ApplicationDbContext _context,IUserContext _userContext, UserManager<ApplicationUser> _userManager) : IRequestHandler<FilterTransactionRecordsQuery, OperationResult<IEnumerable<TransactionRecordResponseDto>>>
    {
      
        public async Task<OperationResult<IEnumerable<TransactionRecordResponseDto>>> Handle(FilterTransactionRecordsQuery request, CancellationToken cancellationToken)
        {
             IQueryable<TransactionRecord> query =  _context.TransactionRecords
                  .Include(tr => tr.TransactionCategory)
                  .Include(tr => tr.PaymentMethod);

            if (request.minAmount.HasValue && request.minAmount.Value > 0)
                query = query.Where(t => t.Amount >= request.minAmount.Value);

            if (request.maxAmount.HasValue && request.maxAmount.Value > 0)
                query = query.Where(t => t.Amount <= request.maxAmount.Value);

            if (request.transactionCategory.HasValue)
                query = query.Where(t => t.TransactionCategoryId == request.transactionCategory.Value);

            if (request.paymentMethod.HasValue)
                query = query.Where(t => t.PaymentMethodId == request.paymentMethod.Value);

            if (request.transactionDate.HasValue && request.transactionDate.Value != DateTime.MinValue)
                query = query.Where(t => t.TransactionDate.Date == request.transactionDate.Value.Date);

            IEnumerable<TransactionRecord> transactionRecordsFromDb = await query.ToListAsync();
            if (!transactionRecordsFromDb.Any())
            {
                throw new NotFoundException("Transaction record not found");
            }
            // Filter for non-admin users
            if (!await IsUserAdmin(_userContext.UserId))
            {
                transactionRecordsFromDb = transactionRecordsFromDb
                    .Where(t => t.CreatedByApplicationUserId == _userContext.UserId)
                    .ToList();
            }
            return new OperationResult<IEnumerable<TransactionRecordResponseDto>>
            {


                Data = transactionRecordsFromDb.ToResponseDtoList(),
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

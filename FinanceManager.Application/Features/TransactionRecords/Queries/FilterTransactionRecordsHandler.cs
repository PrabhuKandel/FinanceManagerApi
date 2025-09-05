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
    public class FilterTransactionRecordsHandler(ApplicationDbContext _context,IUserContext _userContext) : IRequestHandler<FilterTransactionRecordsQuery, OperationResult<IEnumerable<TransactionRecordResponseDto>>>
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

            bool isAdmin = _userContext.IsAdmin();
            // Filter for non-admin users
            if (!isAdmin)
            {
                query = query
                    .Where(t => t.CreatedByApplicationUserId == _userContext.UserId);

            }
            else
            {
                query = query.Include(tr => tr.CreatedByApplicationUser).Include(tr => tr.UpdatedByApplicationUser);
            }

                IEnumerable<TransactionRecord> transactionRecordsFromDb = await query.ToListAsync();
        



            if (!transactionRecordsFromDb.Any())
            {
                throw new NotFoundException("Transaction record not found");
            }
         
           return new OperationResult<IEnumerable<TransactionRecordResponseDto>>
                {


                    Data = transactionRecordsFromDb.ToResponseDtoList(isAdmin),
                    Message = "Transaction records retrieved successfully"

                };
        }

    }
}

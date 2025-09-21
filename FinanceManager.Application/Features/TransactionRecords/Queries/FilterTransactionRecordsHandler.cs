using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Mapping;
using FinanceManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.TransactionRecords.Queries
{
    public class FilterTransactionRecordsHandler : IRequestHandler<FilterTransactionRecordsQuery, OperationResult<IEnumerable<TransactionRecordResponseDto>>>
    {
        private readonly IApplicationDbContext context;
        private readonly IUserContext userContext;

        public FilterTransactionRecordsHandler(IApplicationDbContext _context,IUserContext _userContext)
        {
            context = _context;
            userContext = _userContext;
        }

        public async Task<OperationResult<IEnumerable<TransactionRecordResponseDto>>> Handle(FilterTransactionRecordsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<TransactionRecord> query = context.TransactionRecords
                 .Include(tr => tr.TransactionCategory)
                    .Include(tr => tr.TransactionPayments).ThenInclude(tp => tp.PaymentMethod);
        

            if (request.minAmount.HasValue && request.minAmount.Value > 0)
                query = query.Where(t => t.Amount >= request.minAmount.Value);

            if (request.maxAmount.HasValue && request.maxAmount.Value > 0)
                query = query.Where(t => t.Amount <= request.maxAmount.Value);

            if (request.transactionCategoryId.HasValue)
                query = query.Where(t => t.TransactionCategoryId == request.transactionCategoryId.Value);

            // Filter by payments
            if (request.paymentMethodId.HasValue)
                query = query.Where(tr => tr.TransactionPayments
                    .Any(tp => tp.PaymentMethodId == request.paymentMethodId.Value));


            if (request.transactionDate.HasValue && request.transactionDate.Value != DateTime.MinValue)
                query = query.Where(t => t.TransactionDate.Date == request.transactionDate.Value.Date);

            bool isAdmin = userContext.IsAdmin();
            // Filter for non-admin users
            if (!isAdmin)
            {
                query = query
                    .Where(t => t.CreatedByApplicationUserId == userContext.UserId);

            }
            else
            {
                query = query.Include(tr => tr.CreatedByApplicationUser).Include(tr => tr.UpdatedByApplicationUser);
            }

               var  transactionRecordsFromDb = await query.ToListAsync();
        



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

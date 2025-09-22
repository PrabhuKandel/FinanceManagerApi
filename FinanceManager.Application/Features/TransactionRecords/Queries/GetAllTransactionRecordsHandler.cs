using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.TransactionRecords.Queries
{
    public class GetAllTransactionRecordsHandler : IRequestHandler<GetAllTransactionRecordsQuery, OperationResult<IEnumerable<TransactionRecordResponseDto>>>
    {
        private readonly IApplicationDbContext context;
        private readonly IUserContext userContext;

        public GetAllTransactionRecordsHandler(IApplicationDbContext _context,IUserContext _userContext)
        {
            context = _context;
            userContext = _userContext;
        }

        public async Task<OperationResult<IEnumerable<TransactionRecordResponseDto>>> Handle(GetAllTransactionRecordsQuery request, CancellationToken cancellationToken)
        {
            var query =    context.TransactionRecords
                .Include(tr => tr.TransactionCategory)
                 .Include(tr => tr.TransactionPayments)
                        .ThenInclude(tp => tp.PaymentMethod)
                .Include(t => t.CreatedByApplicationUser)
                 .Include(t => t.UpdatedByApplicationUser)
                 .AsQueryable();

            // Check admin status once
            var isAdmin = userContext.IsAdmin();
            // Filter for non-admin users
            if (!isAdmin)
            {
                query = query
                    .Where(t => t.CreatedByApplicationUserId == userContext.UserId);
          
            }

            // Execute query
            var transactionRecordsFromDb = await query.ToListAsync(cancellationToken);

            var  transactionRecordsDtos =  transactionRecordsFromDb.ToResponseDtoList(isAdmin);
            return new OperationResult<IEnumerable<TransactionRecordResponseDto>>
            {


                Data = transactionRecordsDtos,
                Message = "Transaction records retrieved successfully"

            };



        }
  
    }
}

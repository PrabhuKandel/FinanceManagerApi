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
    public class GetAllTransactionRecordsHandler : IRequestHandler<GetAllTransactionRecordsQuery, PaginatedOperationResult<IEnumerable<TransactionRecordResponseDto>>>
    {
        private readonly IApplicationDbContext context;
        private readonly IUserContext userContext;

        public GetAllTransactionRecordsHandler(IApplicationDbContext _context,IUserContext _userContext)
        {
            context = _context;
            userContext = _userContext;
        }

        public async Task<PaginatedOperationResult<IEnumerable<TransactionRecordResponseDto>>> Handle(GetAllTransactionRecordsQuery request, CancellationToken cancellationToken)
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


            //for pagination
            var totalCount = await query.CountAsync(cancellationToken);
            var skip = (request.PageNumber - 1) * request.PageSize;
            var transactionRecordsFromDb = await query
                .Skip(skip)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var  transactionRecordsDtos =  transactionRecordsFromDb.ToResponseDtoList(isAdmin);
            return new PaginatedOperationResult<IEnumerable<TransactionRecordResponseDto>>
            {


                Data = transactionRecordsDtos,
                Message = "Transaction records retrieved successfully",
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize

            };



        }
  
    }
}

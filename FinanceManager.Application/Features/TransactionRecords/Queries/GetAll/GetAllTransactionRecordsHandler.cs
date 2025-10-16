using FinanceManager.Application.Common;
using System.Linq.Dynamic.Core;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FinanceManager.Application.Features.TransactionRecords.Dtos;
using FinanceManager.Application.Features.TransactionRecords.Mapping;

namespace FinanceManager.Application.Features.TransactionRecords.Queries.GetAll
{
    public class GetAllTransactionRecordsHandler : IRequestHandler<GetAllTransactionRecordsQuery, PaginatedOperationResult<IEnumerable<TransactionRecordResponseDto>>>
    {
        private readonly IApplicationDbContext context;
        private readonly IUserContext userContext;

        // Mapping frontend sort keys to backend properties
        private static readonly Dictionary<string, string> SortColumnMap = new(StringComparer.OrdinalIgnoreCase)
    {
        { "transactionDate", "TransactionDate" },
        { "amount", "Amount" },
        { "category", "TransactionCategory.Name" },
        { "createdBy", "CreatedByApplicationUser.Email" },
        { "updatedBy", "UpdatedByApplicationUser.Email" }
    };
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
                 .Include(t=>t.ActionedByApplicationUser)
                 .AsQueryable();

            // Check admin status once
            var isAdmin = userContext.IsAdmin();
            // Filter for non-admin users
            if (!isAdmin)
            {
                query = query
                    .Where(t => t.CreatedByApplicationUserId == userContext.UserId);
          
            }
            if (request.FromDate.HasValue)
                query = query.Where(t => t.TransactionDate >= request.FromDate.Value);

            if (request.ToDate.HasValue)
                query = query.Where(t => t.TransactionDate <= request.ToDate.Value);

            if (!string.IsNullOrEmpty(request.CreatedBy))
                query = query.Where(x => x.CreatedByApplicationUserId == request.CreatedBy);

            if (!string.IsNullOrEmpty(request.UpdatedBy))
                query = query.Where(x => x.UpdatedByApplicationUserId == request.UpdatedBy);

            if (request.ApprovalStatus.HasValue)
                query = query.Where(x => x.ApprovalStatus == request.ApprovalStatus);

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim().ToLower();
                query = query.Where(t =>
                    (t.Description ?? string.Empty).ToLower().Contains(search) ||
                    t.TransactionCategory!.Name.ToLower().Contains(search) ||
                    t.CreatedByApplicationUser!.Email!.ToLower().Contains(search) ||
                    (t.UpdatedByApplicationUser != null?t.UpdatedByApplicationUser.Email??string.Empty:string.Empty).ToLower().Contains(search)
                );
            }

            // Apply sorting safely
            var sortColumn = SortColumnMap.ContainsKey(request.SortBy ?? "")
                ? SortColumnMap[request.SortBy!]
                : "TransactionDate"; // default sort

            var sortDirection = request.SortDescending ? "descending" : "ascending";
            query = query.OrderBy($"{sortColumn} {sortDirection}");


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

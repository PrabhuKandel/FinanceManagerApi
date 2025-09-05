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
    public class GetAllTransactionRecordsHandler(ApplicationDbContext _context,IUserContext _userContext) : IRequestHandler<GetAllTransactionRecordsQuery, OperationResult<IEnumerable<TransactionRecordResponseDto>>>
    {
        public async Task<OperationResult<IEnumerable<TransactionRecordResponseDto>>> Handle(GetAllTransactionRecordsQuery request, CancellationToken cancellationToken)
        {
            var transactionRecordsFromDb =  await  _context.TransactionRecords
                .Include(tr => tr.TransactionCategory)
                .Include(tr => tr.PaymentMethod)
                .Include(t => t.CreatedByApplicationUser)
                 .Include(t => t.UpdatedByApplicationUser).ToListAsync();
            if (!transactionRecordsFromDb.Any())
            {
                throw new NotFoundException("Transaction record doesn't exist");
            }
            // Check admin status once
            var isAdmin = _userContext.IsAdmin();
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
  
    }
}

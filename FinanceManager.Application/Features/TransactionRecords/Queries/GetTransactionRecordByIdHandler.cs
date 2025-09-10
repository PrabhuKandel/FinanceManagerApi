using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Mapping;
using FinanceManager.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.TransactionRecords.Queries
{
    public class GetTransactionRecordByIdHandler : IRequestHandler<GetTransactionRecordByIdQuery, OperationResult<TransactionRecordResponseDto>>
    {
        private readonly ApplicationDbContext context;
        private readonly IUserContext userContext;

        public GetTransactionRecordByIdHandler(ApplicationDbContext _context,IUserContext _userContext)
        {
            context = _context;
            userContext = _userContext;
        }

        public async Task<OperationResult<TransactionRecordResponseDto>> Handle(GetTransactionRecordByIdQuery request, CancellationToken cancellationToken)
        {
            
            var transactionRecord = await context.TransactionRecords
                .Include(tr => tr.TransactionCategory)
                .Include(tr => tr.PaymentMethod)
                .Include(t => t.CreatedByApplicationUser)
                .Include(t => t.UpdatedByApplicationUser).FirstOrDefaultAsync(tr => tr.Id == request.Id);

            var isAdmin = userContext.IsAdmin();
            // Filter for non-admin users
            if (!isAdmin)
            {
                if (transactionRecord?.CreatedByApplicationUserId != userContext.UserId)
                {
                    throw new AuthorizationException("You can't access this record.");
                }
            }
                var transactionRecordDto =   transactionRecord?.ToResponseDto(isAdmin);
             return new OperationResult<TransactionRecordResponseDto>
            {

                Data = transactionRecordDto,
                Message = "Transaction record  retrieved successfully"


            };

        }

    }
}

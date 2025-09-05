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

namespace FinanceManager.Application.Features.TransactionRecords.Commands
{
    public class UpdateTransactionRecordHandler : IRequestHandler<UpdateTransactionRecordCommand, OperationResult<TransactionRecordResponseDto>>
    {
        private readonly ApplicationDbContext context;
        private readonly IUserContext userContext;

        public UpdateTransactionRecordHandler(ApplicationDbContext _context, IUserContext _userContext)
        {
            context = _context;
            userContext = _userContext;
        }

        public async Task<OperationResult<TransactionRecordResponseDto>> Handle(UpdateTransactionRecordCommand request, CancellationToken cancellationToken)
        {

            var transactionRecordFromDb = await context.TransactionRecords.FindAsync(request.Id);
            if (transactionRecordFromDb == null)
            {
                throw new NotFoundException("Transaction record doesn't exist");
            }

            if (!userContext.IsAdmin())
            {
                if (transactionRecordFromDb.CreatedByApplicationUserId != userContext.UserId)
                {
                    throw new AuthorizationException("You can't access this record.");
                }
            }

            if (!await context.TransactionCategories.AnyAsync(c => c.Id == request.transactionRecord.TransactionCategoryId))
                throw new BusinessValidationException("Invalid Transaction Category");

            if (!await context.PaymentMethods.AnyAsync(c => c.Id == request.transactionRecord.PaymentMethodId))
                throw new BusinessValidationException("Invalid Payment Method");

            transactionRecordFromDb.UpdatedByApplicationUserId = userContext.UserId;


            transactionRecordFromDb.UpdateEntity(request.transactionRecord);

            await context.SaveChangesAsync();

            var transactionRecord = await context.TransactionRecords
           .Include(t => t.TransactionCategory)
           .Include(t => t.PaymentMethod)
           .Include(t => t.CreatedByApplicationUser)
           .Include(t => t.UpdatedByApplicationUser)
           .FirstAsync(t => t.Id == request.Id, cancellationToken);

            return new OperationResult<TransactionRecordResponseDto>
            {

                Message = "Transaction category updated",
                Data = transactionRecord.ToResponseDto(userContext.IsAdmin())
            };

        }


    }
}

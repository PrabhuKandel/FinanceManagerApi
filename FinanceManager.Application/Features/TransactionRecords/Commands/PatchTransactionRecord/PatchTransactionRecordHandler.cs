using Ardalis.GuardClauses;
using FinanceManager.Application.Common;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Features.TransactionRecords.Dtos;
using FinanceManager.Application.Features.TransactionRecords.Mapping;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Mapping;
using FinanceManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.TransactionRecords.Commands.PatchTransactionRecord
{
    public class PatchTransactionRecordHandler : IRequestHandler<PatchTransactionRecordCommand, OperationResult<TransactionRecordResponseDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IUserContext userContext;

        public PatchTransactionRecordHandler( IApplicationDbContext _context, IUserContext _userContext)
        {
            context = _context;
            userContext = _userContext;
        }

        public async Task<OperationResult<TransactionRecordResponseDto>> Handle(PatchTransactionRecordCommand request, CancellationToken cancellationToken)
        {
            var transactionRecordFromDb = await context.TransactionRecords
                    .Include(tr => tr.TransactionPayments)
                    .FirstOrDefaultAsync(tr => tr.Id == request.Id, cancellationToken);
            Guard.Against.Null(transactionRecordFromDb, nameof(transactionRecordFromDb), "Transaction record not found");

            // Authorization check
            if (!userContext.IsAdmin() && transactionRecordFromDb.CreatedByApplicationUserId != userContext.UserId)
            {
                
                   throw new AuthorizationException();
                
            }
            //validating amount
            decimal totalAmount = request.Amount ?? transactionRecordFromDb.Amount;

            var updatedPayments = request.Payments != null && request.Payments.Any()
                ? request.Payments.Select(p => p.Amount).Sum()
                : transactionRecordFromDb.TransactionPayments.Sum(p => p.Amount);

            if (totalAmount != updatedPayments)
            {
                throw new BusinessValidationException("Total transaction amount must equal sum of payments");
            }




            transactionRecordFromDb.TransactionCategoryId = request.TransactionCategoryId ?? transactionRecordFromDb.TransactionCategoryId;
            transactionRecordFromDb.Amount = request.Amount ?? transactionRecordFromDb.Amount;
            transactionRecordFromDb.Description = request.Description ?? transactionRecordFromDb.Description;
            transactionRecordFromDb.TransactionDate = request.TransactionDate ?? transactionRecordFromDb.TransactionDate;
            transactionRecordFromDb.UpdatedByApplicationUserId = userContext.UserId;
            // Handle transaction payments (replace all if provided)
            if (request.Payments != null && request.Payments.Any())
            {
                // Remove existing payments
                context.TransactionPayments.RemoveRange(transactionRecordFromDb.TransactionPayments);

                // Add new payments
                transactionRecordFromDb.TransactionPayments = request.Payments
                    .Select(p => new TransactionPayment
                    {
                        PaymentMethodId = p.PaymentMethodId,
                        Amount = p.Amount
                    })
                    .ToList();
            }


            await context.SaveChangesAsync(cancellationToken);


              var transactionRecord = await context.TransactionRecords
             .Include(t => t.TransactionCategory)
             .Include(t => t.TransactionPayments)
                 .ThenInclude(tp => tp.PaymentMethod)
             .Include(t => t.CreatedByApplicationUser)
             .Include(t => t.UpdatedByApplicationUser)
             .FirstAsync(t => t.Id == request.Id, cancellationToken);

            return new OperationResult<TransactionRecordResponseDto>
            {
                Message = "Transaction record patched",
                Data = transactionRecord.ToResponseDto(userContext.IsAdmin())
            };
        }



  
    }
}

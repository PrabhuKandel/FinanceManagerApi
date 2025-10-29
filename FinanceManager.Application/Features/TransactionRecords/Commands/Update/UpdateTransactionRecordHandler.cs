using Ardalis.GuardClauses;
using FinanceManager.Application.Common;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Features.TransactionRecords.Dtos;
using FinanceManager.Application.Features.TransactionRecords.Mapping;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace FinanceManager.Application.Features.TransactionRecords.Commands.Update
{
    public class UpdateTransactionRecordHandler : IRequestHandler<UpdateTransactionRecordCommand, OperationResult<TransactionRecordResponseDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IUserContext userContext;

        public UpdateTransactionRecordHandler(IApplicationDbContext _context, IUserContext _userContext)
        {
            context = _context;
            userContext = _userContext;
        }

        public async Task<OperationResult<TransactionRecordResponseDto>> Handle(UpdateTransactionRecordCommand request, CancellationToken cancellationToken)
        {
            var errors = new Dictionary<string, string[]>();

            // Load TransactionRecord with related payments
            var transactionRecord = await context.TransactionRecords
                .Include(tr => tr.TransactionPayments)
                .FirstOrDefaultAsync(tr => tr.Id == request.Id, cancellationToken);

            Guard.Against.Null(transactionRecord, nameof(transactionRecord), "Transaction record not found");

            if (!userContext.IsAdmin() && transactionRecord?.CreatedByApplicationUserId != userContext.UserId)
                throw new AuthorizationException("You can't access this record.");


            //optimization remaining.....
            // Preload active payment method IDs
            var activePaymentMethodIds = await context.PaymentMethods
                .Where(pm => pm.IsActive)
                .Select(pm => pm.Id)
                .ToHashSetAsync(cancellationToken);

            // Loop through incoming payments
            for (int i = 0; i < request.Payments.Count; i++)
            {
                var payment = request.Payments[i];

                // Skip existing payments
                if (transactionRecord.TransactionPayments.Any(tp => tp.PaymentMethodId == payment.PaymentMethodId))
                    continue;

                // PaymentMethodId validation
                if (!activePaymentMethodIds.Contains(payment.PaymentMethodId))
                    errors[$"TransactionRecord.Payments[{i}].PaymentMethodId"] = new[] { "Invalid or inactive payment method" };

                // Amount validation
                if (payment.Amount <= 0)
                    errors[$"TransactionRecord.Payments[{i}].Amount"] = new[] { "Payment amount must be greater than 0" };
            }

            // Throw if any errors
            if (errors.Any())
                throw new BusinessValidationException(errors);


        
           

            transactionRecord.UpdatedByApplicationUserId = userContext.UserId;
            transactionRecord.UpdatedAt = DateTime.UtcNow;

            //update transaction record 
            transactionRecord.UpdateEntity(request);


            //remove existing payments
            context.TransactionPayments.RemoveRange(transactionRecord.TransactionPayments);

              transactionRecord.TransactionPayments = request.Payments
                        .Select(p => new TransactionPayment
                        {
                            PaymentMethodId = p.PaymentMethodId,
                            Amount = p.Amount
                        })
                        .ToList();


            await context.SaveChangesAsync();


            var savedEntity = await context.TransactionRecords
           .Include(t => t.TransactionCategory)
           .Include(t => t.TransactionPayments)
                .ThenInclude(tp => tp.PaymentMethod)
           .Include(t => t.CreatedByApplicationUser)
           .Include(t => t.UpdatedByApplicationUser)
           .FirstAsync(t => t.Id == request.Id, cancellationToken);

            return new OperationResult<TransactionRecordResponseDto>
            {

                Message = "Transaction category updated",
                Data = savedEntity.ToResponseDto(userContext.IsAdmin())
            };

        }


    }
}

using FinanceManager.Application.Common;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Enums;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Commands.BulkCreate
{
    public class BulkCreateTransactionRecordHandler(IApplicationDbContext context, IUserContext userContext) : IRequestHandler<BulkCreateTransactionRecordCommand, OperationResult<string>>
    {
        public  async Task<OperationResult<string>> Handle(BulkCreateTransactionRecordCommand request, CancellationToken cancellationToken)
        {
            var transactionEntities = new List<TransactionRecord>();
            foreach (var row in request.TransactionRecords)
            {
                var entity = new TransactionRecord
                {
                    TransactionCategoryId = row.TransactionCategoryId,
                    Amount = row.Amount,
                    Description = row.Description,
                    TransactionDate = row.TransactionDate,
                    ApprovalStatus = userContext.IsAdmin() ? TransactionRecordApprovalStatus.Approved : TransactionRecordApprovalStatus.Pending,
                    CreatedByApplicationUserId = userContext.UserId,
                    UpdatedByApplicationUserId = userContext.UserId,
                    TransactionPayments = row.Payments.Select(p => new TransactionPayment
                    {
                        PaymentMethodId = p.PaymentMethodId,
                        Amount = p.Amount
                    }).ToList()
                };

                transactionEntities.Add(entity);
            }

            await context.TransactionRecords.AddRangeAsync(transactionEntities);
            await context.SaveChangesAsync();

            return new OperationResult<string>
            {
                Message = "Bulk transaction records created successfully."
            };
        }
    }
}

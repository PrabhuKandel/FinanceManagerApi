using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Mapping;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.TransactionRecords.Commands
{
    public class CreateTransactionRecordHandler : IRequestHandler<CreateTransactionRecordCommand, OperationResult<TransactionRecordResponseDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IUserContext userContext;
        private readonly ITransactionAttachmentService transactionAttachmentService;
        public CreateTransactionRecordHandler(IApplicationDbContext _context, IUserContext _userContext, ITransactionAttachmentService transactionAttachmentService)
        {
            context = _context;
            userContext = _userContext;
            this.transactionAttachmentService = transactionAttachmentService;
        }

        public async Task<OperationResult<TransactionRecordResponseDto>> Handle(CreateTransactionRecordCommand request, CancellationToken cancellationToken)
        {

            // Start a transaction
             await using var transaction = await context.Database.BeginTransactionAsync();

                var entity = request.TransactionRecord.ToEntity(userContext.UserId);

            // Set ApprovalStatus based on user role
            entity.ApprovalStatus = userContext.IsAdmin()
                ? TransactionRecordApprovalStatus.Approved
                : TransactionRecordApprovalStatus.Pending;



            await context.TransactionRecords.AddAsync(entity);

    

            //throw new Exception("Testing rollback!");

            // Add transaction payments
            if (request.TransactionRecord.Payments != null && request.TransactionRecord.Payments.Any())
            {
                var payments = request.TransactionRecord.Payments
                    .Select(p => new TransactionPayment
                    {
                        TransactionRecordId = entity.Id,
                        PaymentMethodId = p.PaymentMethodId,
                        Amount = p.Amount
                    }).ToList();

                await context.TransactionPayments.AddRangeAsync(payments);
             
            }

            // **Save attachments if any**
            if (request.TransactionRecord.TransactionAttachments?.Any() == true)
            {
                await transactionAttachmentService.SaveAttachmentsAsync(
                    entity.Id,                          
                    request.TransactionRecord.TransactionAttachments,
                    userContext.UserId                    
                );
            }

            await context.SaveChangesAsync();
            //Commit transaction
            await transaction.CommitAsync();

            var savedEntity =  await context.TransactionRecords
                .Include(tr => tr.TransactionCategory)
                .Include(tr => tr.TransactionPayments)
                .ThenInclude(tp => tp.PaymentMethod)
                .Include(tr=>tr.CreatedByApplicationUser)
                .Include(tr=>tr.UpdatedByApplicationUser)
                 .FirstOrDefaultAsync(tr => tr.Id == entity.Id);



            return new OperationResult<TransactionRecordResponseDto>
            {

                Message = "New transaction category added",
                Data = savedEntity?.ToResponseDto(userContext.IsAdmin())
            };

            
        }
    }
}

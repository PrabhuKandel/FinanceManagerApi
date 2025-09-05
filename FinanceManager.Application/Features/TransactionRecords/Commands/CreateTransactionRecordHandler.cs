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
    public class CreateTransactionRecordHandler : IRequestHandler<CreateTransactionRecordCommand, OperationResult<TransactionRecordResponseDto>>
    {
        private readonly ApplicationDbContext context;
        private readonly IUserContext userContext;

        public CreateTransactionRecordHandler(ApplicationDbContext _context, IUserContext _userContext)
        {
            context = _context;
            userContext = _userContext;
        }

        public async Task<OperationResult<TransactionRecordResponseDto>> Handle(CreateTransactionRecordCommand request, CancellationToken cancellationToken)
        {


            if (!await context.TransactionCategories.AnyAsync(c => c.Id == request.transactionRecord.TransactionCategoryId))
                throw new BusinessValidationException("Invalid Transaction Category");

            if (!await context.PaymentMethods.AnyAsync(c => c.Id == request.transactionRecord.PaymentMethodId))
                throw new BusinessValidationException("Invalid Payment Method");

            var entity = request.transactionRecord.ToEntity();
            entity.CreatedByApplicationUserId = userContext.UserId;
            entity.UpdatedByApplicationUserId = userContext.UserId;

            await context.TransactionRecords.AddAsync(entity);
            await context.SaveChangesAsync();

            var savedEntity =  await context.TransactionRecords
                .Include(tr => tr.TransactionCategory)
                .Include(tr => tr.PaymentMethod)
                .Include(tr=>tr.CreatedByApplicationUser)
                .Include(tr=>tr.UpdatedByApplicationUser)
                 .FirstOrDefaultAsync(tr => tr.Id == entity.Id);



            return new OperationResult<TransactionRecordResponseDto>
            {

                Message = "New transaction category added",
                Data = savedEntity?.ToResponseDto(userContext.IsAdmin())
            };

            ;
        }
    }
}

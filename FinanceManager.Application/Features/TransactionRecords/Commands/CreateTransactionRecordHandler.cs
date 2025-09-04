using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Mapping;
using FinanceManager.Domain.Entities;
using FinanceManager.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.TransactionRecords.Commands
{
    public class CreateTransactionRecordHandler(ApplicationDbContext _context, IUserContext _userContext) : IRequestHandler<CreateTransactionRecordCommand, TransactionRecordResponseDto>
    {
        public async Task<TransactionRecordResponseDto> Handle(CreateTransactionRecordCommand request, CancellationToken cancellationToken)
        {


            if (!await _context.TransactionCategories.AnyAsync(c => c.Id == request.transactionRecord.TransactionCategoryId))
                throw new CustomValidationException("Invalid Transaction Category");

            if (!await _context.TransactionCategories.AnyAsync(c => c.Id == request.transactionRecord.PaymentMethodId))
                throw new CustomValidationException("Invalid Payment Method");

            var entity = request.transactionRecord.ToEntity();
            entity.CreatedByApplicationUserId = _userContext.UserId;
            entity.UpdatedByApplicationUserId = _userContext.UserId;

            await _context.TransactionRecords.AddAsync(entity);
            await _context.SaveChangesAsync();

            var savedEntity =  await _context.TransactionRecords
                .Include(tr => tr.TransactionCategory)
                .Include(tr => tr.PaymentMethod)
                 .FirstOrDefaultAsync(tr => tr.Id == entity.Id);

            return savedEntity.ToResponseDto()

            ;
        }
    }
}

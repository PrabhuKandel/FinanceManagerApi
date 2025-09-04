using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Mapping;
using FinanceManager.Domain.Entities;
using FinanceManager.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.TransactionCategory.Commands
{
    public  class CreateTransactionCategoryHandler(ApplicationDbContext _context) : IRequestHandler<CreateTransactionCategoryCommand, TransactionCategoryResponseDto>
    {

        public async Task<TransactionCategoryResponseDto> Handle(CreateTransactionCategoryCommand request, CancellationToken cancellationToken)
        {


            if (await  _context.TransactionCategories.AnyAsync(c => c.Name == request.transactionCategory.Name))
                throw new CustomValidationException("Transaction category  with this name already exists.");

            var entity = request.transactionCategory.ToEntity();
            await _context.TransactionCategories.AddAsync(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.ToResponseDto();


        }
    }

}
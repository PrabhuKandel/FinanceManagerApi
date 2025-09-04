using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Mapping;
using FinanceManager.Domain.Entities;
using FinanceManager.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.TransactionCategory.Commands.CreateTransactionCategory
{
    public  class UpdateTransactionCategoryHandler(ApplicationDbContext _context) : IRequestHandler<UpdateTransactionCategoryCommand, TransactionCategoryResponseDto>
    {

        public async Task<TransactionCategoryResponseDto> Handle(UpdateTransactionCategoryCommand request, CancellationToken cancellationToken)
        {

            var transactionCategory = await _context.TransactionCategories.FindAsync(request.Id);
            if (transactionCategory == null)
            {
                throw new NotFoundException("Transaction Category doesn't exist");
            }

            transactionCategory.UpdateEntity(request.transactionCategory);

            return transactionCategory.ToResponseDto();


        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Mapping;
using FinanceManager.Infrastructure.Data;
using MediatR;

namespace FinanceManager.Application.Features.TransactionCategory.Queries
{
    public class GetTransactionCategoryByIdHandler(ApplicationDbContext _context) : IRequestHandler<GetTransactionCategoryByIdQuery, TransactionCategoryResponseDto>
    {


        public async Task<TransactionCategoryResponseDto> Handle(GetTransactionCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var transactionCategory = await _context.TransactionCategories.FindAsync(request.Id);
            if (transactionCategory == null)
            {
                throw new NotFoundException("Transaction category not found");
            }

            var transactionCategoryDto = transactionCategory.ToResponseDto();
            
            return transactionCategory.ToResponseDto();
        }
    }
}
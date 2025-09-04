using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Mapping;
using FinanceManager.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.TransactionCategories.Queries
{
    public class GetAllTransactionCategoriesHandler(ApplicationDbContext _context) : IRequestHandler<GetAllTransactionCategoriesQuery, OperationResult<IEnumerable<TransactionCategoryResponseDto>>>
    {
    
        public async Task<OperationResult<IEnumerable<TransactionCategoryResponseDto>>>Handle(GetAllTransactionCategoriesQuery request, CancellationToken cancellationToken)
        {
            var transactionCategories = await _context.TransactionCategories.ToListAsync();
            var transactionCategoriesDtos = transactionCategories.ToResponseDtoList();

            return new OperationResult<IEnumerable<TransactionCategoryResponseDto>>
            {


                Data = transactionCategoriesDtos,
                Message = transactionCategoriesDtos.Any() ? "Transaction categories retrieved successfully" : "  No Transaction categories "

            };
        }
    }
}
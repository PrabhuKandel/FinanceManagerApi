using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Mapping;
using FinanceManager.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.TransactionCategory.Queries
{
    public class GetAllTransactionCategoriesHandler(ApplicationDbContext _context) : IRequestHandler<GetAllTransactionCategoriesQuery, IEnumerable<TransactionCategoryResponseDto>>
    {
    
        public async Task<IEnumerable<TransactionCategoryResponseDto>>Handle(GetAllTransactionCategoriesQuery request, CancellationToken cancellationToken)
        {
            var transactionCategorys = await _context.TransactionCategories.ToListAsync();

            return transactionCategorys.ToResponseDtoList();
        }
    }
}
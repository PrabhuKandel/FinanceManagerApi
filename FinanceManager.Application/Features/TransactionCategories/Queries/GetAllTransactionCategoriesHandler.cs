using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.TransactionCategories.Queries
{
    public class GetAllTransactionCategoriesHandler : IRequestHandler<GetAllTransactionCategoriesQuery, OperationResult<IEnumerable<TransactionCategoryResponseDto>>>
    {
        private readonly IApplicationDbContext context;

        public GetAllTransactionCategoriesHandler(IApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<OperationResult<IEnumerable<TransactionCategoryResponseDto>>>Handle(GetAllTransactionCategoriesQuery request, CancellationToken cancellationToken)
        {
            var transactionCategories = await context.TransactionCategories.ToListAsync();
            var transactionCategoriesDtos = transactionCategories.ToResponseDtoList();

            return new OperationResult<IEnumerable<TransactionCategoryResponseDto>>
            {


                Data = transactionCategoriesDtos,
                Message = "Transaction categories retrieved successfully"
            };
        }
    }
}
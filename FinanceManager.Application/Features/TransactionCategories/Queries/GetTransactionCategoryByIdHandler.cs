using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Mapping;
using FinanceManager.Infrastructure.Data;
using MediatR;

namespace FinanceManager.Application.Features.TransactionCategories.Queries
{
    public class GetTransactionCategoryByIdHandler : IRequestHandler<GetTransactionCategoryByIdQuery, OperationResult<TransactionCategoryResponseDto>>
    {
        private readonly ApplicationDbContext context;

        public GetTransactionCategoryByIdHandler(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<OperationResult<TransactionCategoryResponseDto>> Handle(GetTransactionCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var transactionCategory = await context.TransactionCategories.FindAsync(request.Id);

            return new OperationResult<TransactionCategoryResponseDto>
            {

                Data = transactionCategory?.ToResponseDto(),
                Message = "Transaction category  retrieved successfully"


            };
        }
    }
}
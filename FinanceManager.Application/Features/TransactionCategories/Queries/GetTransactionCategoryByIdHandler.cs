using Ardalis.GuardClauses;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Mapping;
using MediatR;

namespace FinanceManager.Application.Features.TransactionCategories.Queries
{
    public class GetTransactionCategoryByIdHandler : IRequestHandler<GetTransactionCategoryByIdQuery, OperationResult<TransactionCategoryResponseDto>>
    {
        private readonly IApplicationDbContext context;

        public GetTransactionCategoryByIdHandler(IApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<OperationResult<TransactionCategoryResponseDto>> Handle(GetTransactionCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var transactionCategory = await context.TransactionCategories.FindAsync(request.Id);
            Guard.Against.Null(transactionCategory, nameof(transactionCategory), "Transaction category not found");

            return new OperationResult<TransactionCategoryResponseDto>
            {

                Data = transactionCategory?.ToResponseDto(),
                Message = "Transaction category  retrieved successfully"


            };
        }
    }
}
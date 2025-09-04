using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Mapping;
using FinanceManager.Infrastructure.Data;
using MediatR;

namespace FinanceManager.Application.Features.TransactionCategories.Queries
{
    public class GetTransactionCategoryByIdHandler(ApplicationDbContext _context) : IRequestHandler<GetTransactionCategoryByIdQuery, OperationResult<TransactionCategoryResponseDto>>
    {


        public async Task<OperationResult<TransactionCategoryResponseDto>> Handle(GetTransactionCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var transactionCategory = await _context.TransactionCategories.FindAsync(request.Id);
            if (transactionCategory == null)
            {
                throw new NotFoundException("Transaction category not found");
            }

            var transactionCategoryDto = transactionCategory.ToResponseDto();

            return new OperationResult<TransactionCategoryResponseDto>
            {

                Data = transactionCategory.ToResponseDto(),
                Message = "Transaction category  retrieved successfully"


            };
        }
    }
}
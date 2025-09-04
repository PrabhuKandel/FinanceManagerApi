using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Mapping;
using FinanceManager.Infrastructure.Data;
using MediatR;

namespace FinanceManager.Application.Features.TransactionCategories.Commands.CreateTransactionCategory
{
    public  class UpdateTransactionCategoryHandler(ApplicationDbContext _context) : IRequestHandler<UpdateTransactionCategoryCommand, OperationResult<TransactionCategoryResponseDto>>
    {

        public async Task<OperationResult<TransactionCategoryResponseDto>> Handle(UpdateTransactionCategoryCommand request, CancellationToken cancellationToken)
        {

            var transactionCategory = await _context.TransactionCategories.FindAsync(request.Id);
            if (transactionCategory == null)
            {
                throw new NotFoundException("Transaction Category doesn't exist");
            }

            transactionCategory.UpdateEntity(request.transactionCategory);
            return new OperationResult<TransactionCategoryResponseDto>
            {

                Message = "Transaction category updated",
                Data = transactionCategory.ToResponseDto()
            };


        }
    }

}
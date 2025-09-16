using Ardalis.GuardClauses;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Mapping;
using MediatR;

namespace FinanceManager.Application.Features.TransactionCategories.Commands.CreateTransactionCategory
{
    public  class UpdateTransactionCategoryHandler : IRequestHandler<UpdateTransactionCategoryCommand, OperationResult<TransactionCategoryResponseDto>>
    {
        private readonly IApplicationDbContext context;

        public UpdateTransactionCategoryHandler(IApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<OperationResult<TransactionCategoryResponseDto>> Handle(UpdateTransactionCategoryCommand request, CancellationToken cancellationToken)
        {

            var transactionCategory = await context.TransactionCategories.FindAsync(request.Id);
            Guard.Against.Null(transactionCategory, nameof(transactionCategory), "Transaction category not found");
            transactionCategory.UpdateEntity(request.TransactionCategory);
            return new OperationResult<TransactionCategoryResponseDto>
            {

                Message = "Transaction category updated",
                Data = transactionCategory.ToResponseDto()
            };


        }
    }

}
using Ardalis.GuardClauses;
using FinanceManager.Application.Common;
using FinanceManager.Application.Features.TransactionCategories.Dtos;
using FinanceManager.Application.Features.TransactionCategories.Mapping;
using FinanceManager.Application.Interfaces;
using MediatR;

namespace FinanceManager.Application.Features.TransactionCategories.Commands.Update
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
            transactionCategory.UpdateEntity(request);
            return new OperationResult<TransactionCategoryResponseDto>
            {

                Message = "Transaction category updated",
                Data = transactionCategory.ToResponseDto()
            };


        }
    }

}
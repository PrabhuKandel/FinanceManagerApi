using Ardalis.GuardClauses;
using FinanceManager.Application.Common;
using FinanceManager.Application.Interfaces;
using MediatR;

namespace FinanceManager.Application.Features.TransactionCategories.Commands
{
    public class DeleteTransactionCategoryHandler : IRequestHandler<DeleteTransactionCategoryCommand, OperationResult<string>>
    {
        private readonly IApplicationDbContext context;

        public DeleteTransactionCategoryHandler(IApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<OperationResult<string>> Handle(DeleteTransactionCategoryCommand request, CancellationToken cancellationToken)
        {
            var transactionCategory = await context.TransactionCategories.FindAsync(request.Id);
            Guard.Against.Null(transactionCategory, nameof(transactionCategory), "Transaction category not found");
            context.TransactionCategories.Remove(transactionCategory);
            await context.SaveChangesAsync(cancellationToken);
            return new OperationResult<string>
            {

                Message = "Transaction category deleted",

            };
        }
    }
}

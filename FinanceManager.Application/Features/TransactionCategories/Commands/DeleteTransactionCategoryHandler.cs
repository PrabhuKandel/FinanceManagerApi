using FinanceManager.Application.Common;
using FinanceManager.Application.Exceptions;
using FinanceManager.Infrastructure.Data;
using MediatR;

namespace FinanceManager.Application.Features.TransactionCategories.Commands
{
    public class DeleteTransactionCategoryHandler : IRequestHandler<DeleteTransactionCategoryCommand, OperationResult<string>>
    {
        private readonly ApplicationDbContext context;

        public DeleteTransactionCategoryHandler(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<OperationResult<string>> Handle(DeleteTransactionCategoryCommand request, CancellationToken cancellationToken)
        {
            var transactionCategory = await context.TransactionCategories.FindAsync(request.Id);
            if (transactionCategory == null)
            {
                throw new NotFoundException("Transaction Category doesn't exist");
            }
            context.TransactionCategories.Remove(transactionCategory);
            await context.SaveChangesAsync(cancellationToken);
            return new OperationResult<string>
            {

                Message = "Transaction category deleted",

            };
        }
    }
}

using FinanceManager.Application.Exceptions;
using FinanceManager.Infrastructure.Data;
using MediatR;

namespace FinanceManager.Application.Features.TransactionCategories.Commands
{
    public class DeleteTransactionCategoryHandler(ApplicationDbContext _context) : IRequestHandler<DeleteTransactionCategoryCommand, string>
    {
        public async Task<string> Handle(DeleteTransactionCategoryCommand request, CancellationToken cancellationToken)
        {
            var transactionCategory = await _context.TransactionCategories.FindAsync(request.Id);
            if (transactionCategory == null)
            {
                throw new NotFoundException("Transaction Category doesn't exist");
            }
            _context.TransactionCategories.Remove(transactionCategory);
            await _context.SaveChangesAsync(cancellationToken);
            return "Transaction category deleted successfully";
        }
    }
}

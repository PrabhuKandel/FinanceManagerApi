using FinanceManager.Application.Common;
using FinanceManager.Application.Exceptions;
using FinanceManager.Infrastructure.Data;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Commands
{
    public class DeleteTransactionRecordHandler : IRequestHandler<DeleteTransactionRecordCommand, OperationResult<string>>

    {
        private readonly ApplicationDbContext context;

        public DeleteTransactionRecordHandler(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<OperationResult<string>> Handle(DeleteTransactionRecordCommand request, CancellationToken cancellationToken)
        {
            var transactionRecord = await context.TransactionRecords.FindAsync(request.Id);
            if (transactionRecord == null)

            {
                throw new NotFoundException("Transaction record  doesn't exist");
            }
            context.TransactionRecords.Remove(transactionRecord);
            await context.SaveChangesAsync();

            return new OperationResult<String>
            {

                Message = "Transaction record deleted",

            };

        }
    }
}

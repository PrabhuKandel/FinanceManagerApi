using FinanceManager.Application.Common;
using FinanceManager.Application.Exceptions;
using FinanceManager.Infrastructure.Data;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Commands
{
    public class DeleteTransactionRecordHandler(ApplicationDbContext _context) : IRequestHandler<DeleteTransactionRecordCommand, OperationResult<string>>

    {
        public async Task<OperationResult<string>> Handle(DeleteTransactionRecordCommand request, CancellationToken cancellationToken)
        {
            var transactionRecord = await _context.TransactionRecords.FindAsync(request.Id);
            if (transactionRecord == null)

            {
                throw new NotFoundException("Transaction record  doesn't exist");
            }
            _context.TransactionRecords.Remove(transactionRecord);
            await _context.SaveChangesAsync();

            return new OperationResult<String>
            {

                Message = "Transaction record deleted",

            };

        }
    }
}

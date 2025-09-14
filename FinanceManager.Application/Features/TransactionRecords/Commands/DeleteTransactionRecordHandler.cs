using FinanceManager.Application.Common;
using FinanceManager.Application.Interfaces;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Commands
{
    public class DeleteTransactionRecordHandler : IRequestHandler<DeleteTransactionRecordCommand, OperationResult<string>>

    {
        private readonly IApplicationDbContext context;

        public DeleteTransactionRecordHandler(IApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<OperationResult<string>> Handle(DeleteTransactionRecordCommand request, CancellationToken cancellationToken)
        {
            var transactionRecord = await context.TransactionRecords.FindAsync(request.Id);
            context.TransactionRecords.Remove(transactionRecord);
            await context.SaveChangesAsync();

            return new OperationResult<String>
            {

                Message = "Transaction record deleted",

            };

        }
    }
}

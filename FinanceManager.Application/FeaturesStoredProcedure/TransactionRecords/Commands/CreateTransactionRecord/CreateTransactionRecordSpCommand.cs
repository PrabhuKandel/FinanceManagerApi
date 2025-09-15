using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using MediatR;

namespace FinanceManager.Application.FeaturesStoredProcedure.TransactionRecords.Commands.CreateTransactionRecord
{
    public  record CreateTransactionRecordSpCommand(Guid TransactionCategoryId, Guid PaymentMethodId, Decimal Amount, string?Description , DateTime TransactionDate) : IRequest<OperationResult<TransactionRecordResponseDto>>
    {
    }
}

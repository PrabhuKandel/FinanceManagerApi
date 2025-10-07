using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.Application.Features.TransactionRecords.Commands
{
    public class UpdateTransactionRecordCommand : IRequest<OperationResult<TransactionRecordResponseDto>>
    {
        public Guid Id { get; set; }
        public  required TransactionRecordUpdateDto TransactionRecord { get; set; }
    }
}

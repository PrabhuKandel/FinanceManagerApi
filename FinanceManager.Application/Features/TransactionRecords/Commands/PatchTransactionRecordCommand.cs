﻿using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionPayment;
using FinanceManager.Application.Dtos.TransactionRecord;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Commands
{
    public  record PatchTransactionRecordCommand(
        Guid Id,
        Guid? TransactionCategoryId,
        decimal? Amount,
        string? Description,
        DateTime? TransactionDate,
         List<TransactionPaymentDto>? Payments 

        ) : IRequest<OperationResult<TransactionRecordResponseDto>>
    {
    }
}

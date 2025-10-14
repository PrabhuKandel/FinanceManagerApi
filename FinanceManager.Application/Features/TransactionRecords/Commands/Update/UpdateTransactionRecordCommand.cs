﻿using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionPayment;
using FinanceManager.Application.Features.TransactionRecords.Dtos;
using MediatR;



namespace FinanceManager.Application.Features.TransactionRecords.Commands.Update
{
    public record UpdateTransactionRecordCommand 
        (
            Guid Id,
            Guid TransactionCategoryId,
            decimal Amount,
            string? Description,
            DateTime TransactionDate,
            List<TransactionPaymentDto> Payments
            
        )
        : IRequest<OperationResult<TransactionRecordResponseDto>>
    {

   
    }
}

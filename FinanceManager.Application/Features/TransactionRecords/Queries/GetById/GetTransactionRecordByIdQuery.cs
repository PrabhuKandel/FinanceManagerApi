using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Common;
using FinanceManager.Application.Features.TransactionRecords.Dtos;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Queries.GetById
{
    public record GetTransactionRecordByIdQuery(Guid Id) : IRequest<OperationResult<TransactionRecordResponseDto>>
    {
    }
}

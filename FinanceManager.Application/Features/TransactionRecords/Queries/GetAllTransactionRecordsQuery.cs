using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Dtos.TransactionRecord;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Queries
{
    public record GetAllTransactionRecordsQuery:IRequest<IEnumerable<TransactionRecordResponseDto>>
    {
    }
}

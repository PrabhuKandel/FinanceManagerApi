using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FinanceManager.Application.Features.TransactionRecords.Commands
{
    public record DeleteTransactionRecordCommand(Guid Id) : IRequest<string>
    {

    }
}

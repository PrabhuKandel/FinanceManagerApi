
using FinanceManager.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FinanceManager.Application.Features.TransactionRecords.Queries.ImportFromExcel
{
    public class ImportTransactionRecordsQuery : IRequest<OperationResult<string>>
    {
        public required IFormFile ExcelFile { get; set; } 
    }
}

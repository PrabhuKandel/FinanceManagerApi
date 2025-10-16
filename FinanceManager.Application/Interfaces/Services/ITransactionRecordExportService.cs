

using FinanceManager.Application.Features.TransactionRecords.Dtos;

namespace FinanceManager.Application.Interfaces.Services
{
    public interface ITransactionRecordExportService
    {
        byte[] GenerateExcel(IReadOnlyList<TransactionRecordExportDto> records);
    }
}

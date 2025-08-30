using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Domain.Models;

namespace FinanceManager.Application.Interfaces.Services
{
    public interface ITransactionRecordService
    {
        Task<ServiceResponse<IEnumerable<TransactionRecordResponseDto>>> GetAllTransactionRecordsAsync();
        Task<ServiceResponse<TransactionRecordResponseDto>> GetTransactionRecordByIdAsync(Guid id);
        Task<ServiceResponse<TransactionRecordResponseDto>> AddTransactionRecordAsync(TransactionRecordCreateDto transactionRecordCreateDto);
        Task<ServiceResponse<TransactionRecordResponseDto>> UpdateTransactionRecordAsync(Guid id, TransactionRecordUpdateDto transactionRecordUpdateDto);
        Task<ServiceResponse<String>> DeleteTransactionRecordAsync(Guid id);


    }
}

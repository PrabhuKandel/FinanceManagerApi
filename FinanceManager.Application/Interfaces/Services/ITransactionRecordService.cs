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
        Task<ServiceResponse<IEnumerable<TransactionRecordResponseDto>>> GetAllTransactionRecordsAsync(String userId);
        Task<ServiceResponse<TransactionRecordResponseDto>> GetTransactionRecordByIdAsync(Guid id, string userId);
        Task<ServiceResponse<TransactionRecordResponseDto>> AddTransactionRecordAsync(TransactionRecordCreateDto transactionRecordCreateDto, String userId);
        Task<ServiceResponse<TransactionRecordResponseDto>> UpdateTransactionRecordAsync(Guid id, TransactionRecordUpdateDto transactionRecordUpdateDto, string userId);
        Task<ServiceResponse<String>> DeleteTransactionRecordAsync(Guid id, string userId);

        Task<ServiceResponse<IEnumerable<TransactionRecordResponseDto>>> FilterTransactionRecordsAsync(String userId,Decimal? minAmount, Decimal? maxAmount, Guid? transacionCategory, Guid? paymentMethod, DateTime? transactionDate);


    }
}

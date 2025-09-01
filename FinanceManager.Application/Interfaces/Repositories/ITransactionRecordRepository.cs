using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Domain.Models;

namespace FinanceManager.Application.Interfaces.Repositories
{
    public interface ITransactionRecordRepository
    {
      
            Task<IEnumerable<TransactionRecord>> GetAllAsync(String userId);
            Task<TransactionRecord> GetByIdAsync(Guid id , string userId);
            Task  AddAsync(TransactionRecord transactionRecord);
            Task UpdateAsync( TransactionRecord transactionRecord);
            Task DeleteAsync(TransactionRecord transactionRecord);

            Task<IEnumerable<TransactionRecord>> FilterTransactionRecordsAsync(
           String userId,  Decimal? minAmount, Decimal? maxAmount, Guid? transacionCategory, Guid? paymentMethod, DateTime? transactionDate);

        //Task<IEnumerable<TransactionRecord>> GetByCategoryAsync(Guid categoryId); 
    }


}

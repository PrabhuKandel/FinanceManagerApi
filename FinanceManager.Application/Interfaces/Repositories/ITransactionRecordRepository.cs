using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using FinanceManager.Domain.Models;

namespace FinanceManager.Application.Interfaces.Repositories
{
    public interface ITransactionRecordRepository
    {
      
            Task<IEnumerable<TransactionRecord>> GetAllAsync();
            Task<TransactionRecord> GetByIdAsync(Guid id );
            Task  AddAsync(TransactionRecord transactionRecord);
            Task UpdateAsync( TransactionRecord transactionRecord);
            Task DeleteAsync(TransactionRecord transactionRecord);
           
        //Task<IEnumerable<TransactionRecord>> GetByCategoryAsync(Guid categoryId); 
    }


}

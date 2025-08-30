using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using FinanceManager.Domain.Models;

namespace FinanceManager.Application.Interfaces.Repositories
{
    public interface ITransactionCategoryRepository
    {
      
            Task<IEnumerable<TransactionCategory>> GetAllAsync();
            Task<TransactionCategory> GetByIdAsync(Guid id ,bool isTracking = true);
            Task  AddAsync(TransactionCategory transactionCategory);
            Task UpdateAsync( TransactionCategory transactionCategory);
            Task DeleteAsync(TransactionCategory transactionCategory);
            Task<bool> ExistsByNameAsync(string name);
            Task<bool> ExistByIdAsync(Guid id);
        //Task<IEnumerable<TransactionCategory>> GetByCategoryAsync(Guid categoryId); 
    }


}

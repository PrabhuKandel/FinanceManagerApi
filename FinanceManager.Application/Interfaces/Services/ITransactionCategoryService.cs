using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Common;
using FinanceManager.Domain.Models;

namespace FinanceManager.Application.Interfaces.Services
{
    public interface ITransactionCategoryService
    {
        Task<ServiceResponse<IEnumerable<TransactionCategory>>> GetAllTransactionCategoriesAsync();
        Task<ServiceResponse<TransactionCategory>> GetTransactionCategoryByIdAsync(Guid id);
        Task<ServiceResponse<TransactionCategory>> AddTransactionCategoryAsync(TransactionCategory transactionCategory);
        Task<ServiceResponse<TransactionCategory>> UpdateTransactionCategoryAsync(Guid id, TransactionCategory transactionCategory);
        Task<ServiceResponse<String>> DeleteTransactionCategoryAsync(Guid id);


   
    }
}

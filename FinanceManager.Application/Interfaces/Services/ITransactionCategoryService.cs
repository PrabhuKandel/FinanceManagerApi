using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Domain.Models;

namespace FinanceManager.Application.Interfaces.Services
{
    public interface ITransactionCategoryService
    {
        Task<ServiceResponse<IEnumerable<TransactionCategoryResponseDto>>> GetAllTransactionCategoriesAsync();
        Task<ServiceResponse<TransactionCategoryResponseDto>> GetTransactionCategoryByIdAsync(Guid id);
        Task<ServiceResponse<TransactionCategoryResponseDto>> AddTransactionCategoryAsync(TransactionCategoryCreateDto transactionCategoryCreateDto);
        Task<ServiceResponse<TransactionCategoryResponseDto>> UpdateTransactionCategoryAsync(Guid id, TransactionCategoryUpdateDto transactionCategoryUpdateDto);
        Task<ServiceResponse<String>> DeleteTransactionCategoryAsync(Guid id);


   
    }
}

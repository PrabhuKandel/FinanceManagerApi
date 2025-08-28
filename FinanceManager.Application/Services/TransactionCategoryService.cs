using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Common;
using FinanceManager.Application.Interfaces.Repositories;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Domain.Models;

namespace FinanceManager.Application.Services
{
    public class TransactionCategoryService : ITransactionCategoryService
    {
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;
        public TransactionCategoryService(ITransactionCategoryRepository transactionCategoryRepository)
        {
            _transactionCategoryRepository = transactionCategoryRepository;
        
        }
        public async Task<ServiceResponse<TransactionCategory>> AddTransactionCategoryAsync(TransactionCategory transactionCategory)
        {
             await   _transactionCategoryRepository.AddAsync(transactionCategory);

            return new ServiceResponse<TransactionCategory>
            {
                Success = true,
                Message = "New transaction category added",
                Data = transactionCategory
            };
        }

        public async Task<ServiceResponse<String>> DeleteTransactionCategoryAsync(Guid id)

        {
            var transactionCategoryFromDb = await _transactionCategoryRepository.GetByIdAsync(id);
            if(transactionCategoryFromDb==null)

            {
                return new ServiceResponse<String>
                {

                    Success = false,
                    Message = "Transaction category doesn't exist",
                    Data = null
                };
            }
   

          
            await _transactionCategoryRepository.DeleteAsync(transactionCategoryFromDb);

            return new ServiceResponse<String>
            {

                Success = true,
                Message = "Transaction category deleted",
                Data = null
            };

        }

        public async Task<ServiceResponse<IEnumerable<TransactionCategory>>> GetAllTransactionCategoriesAsync()
        {
            var transactionCategories  =  await _transactionCategoryRepository.GetAllAsync();

            if(transactionCategories==null || !transactionCategories.Any())
            {
                return new ServiceResponse<IEnumerable<TransactionCategory>>
                {

                    Success = false,
                    Message = "No  transactions categories found",
                    Data = null
                };
            }

            else
            {
                return new ServiceResponse<IEnumerable<TransactionCategory>>
                {

                    Success = true,
                    Message = "Fetched Successfully",
                    Data = transactionCategories
                };
            }


        }

        public async Task<ServiceResponse<TransactionCategory>> GetTransactionCategoryByIdAsync(Guid id)
        {
            var transactionCategory =  await _transactionCategoryRepository.GetByIdAsync(id);
            if (transactionCategory == null)
            {
                return new ServiceResponse<TransactionCategory>
                {

                    Success = false,
                    Message = "No  transactions category found ",
                    Data = null
                };
            }

            else
            {
                return new ServiceResponse<TransactionCategory>
                {

                    Success = true,
                    Message = "Fetched Successfully",
                    Data = transactionCategory
                };
            }

        }

        public async Task<ServiceResponse<TransactionCategory>> UpdateTransactionCategoryAsync(Guid id, TransactionCategory transactionCategory)
        {
            var transactionCategoryfromDb = await _transactionCategoryRepository.GetByIdAsync(id, isTracking: false);
            if (transactionCategoryfromDb == null)
            {
                return new ServiceResponse<TransactionCategory>
                {
                    Success = false,
                    Message = "Transaction category not found",
                    Data = null
                };
            }
            await _transactionCategoryRepository.UpdateAsync(transactionCategory);
            return new ServiceResponse<TransactionCategory>
            {
                Success = true,
                Message = "Transaction category updated",
                Data = transactionCategory
            };
        }
    }
}

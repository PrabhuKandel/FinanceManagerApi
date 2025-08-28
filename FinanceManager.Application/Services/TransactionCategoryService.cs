using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Interfaces.Repositories;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Mapping;
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
        public async Task<ServiceResponse<IEnumerable<TransactionCategoryResponseDto>>> GetAllTransactionCategoriesAsync()
        {
            var transactionCategories = await _transactionCategoryRepository.GetAllAsync();

            if (transactionCategories == null || !transactionCategories.Any())
            {
                return new ServiceResponse<IEnumerable<TransactionCategoryResponseDto>>
                {

                    Success = false,
                    Message = "No  transactions categories found",
                    Data = null
                };
            }

            var transactionCategoriesDtos = transactionCategories.ToResponseDtoList();


            return new ServiceResponse<IEnumerable<TransactionCategoryResponseDto>>
                {

                    Success = true,
                    Message = "Fetched Successfully",
                    Data = transactionCategoriesDtos
                };
            
        }
        public async Task<ServiceResponse<TransactionCategoryResponseDto>> GetTransactionCategoryByIdAsync(Guid id)
        {
            var transactionCategory = await _transactionCategoryRepository.GetByIdAsync(id);
            if (transactionCategory == null)
            {
                return new ServiceResponse<TransactionCategoryResponseDto>
                {

                    Success = false,
                    Message = "No  transactions category found ",
                    Data = null
                };
            }

            var transcationCategoryDto = transactionCategory.ToResponseDto();


            return new ServiceResponse<TransactionCategoryResponseDto>
            {

                Success = true,
                Message = "Fetched Successfully",
                Data = transcationCategoryDto
            };


        }
        public async Task<ServiceResponse<TransactionCategoryResponseDto>> AddTransactionCategoryAsync(TransactionCategoryCreateDto transactionCategoryCreateDto)
        {
            var entity = transactionCategoryCreateDto.ToEntity();
             await   _transactionCategoryRepository.AddAsync(entity);

            return new ServiceResponse<TransactionCategoryResponseDto>
            {
                Success = true,
                Message = "New transaction category added",
                Data = entity.ToResponseDto()
            };
        }

        public async Task<ServiceResponse<TransactionCategoryResponseDto>> UpdateTransactionCategoryAsync(Guid id, TransactionCategoryUpdateDto transactionCategoryUpdateDto)
        {
            var transactionCategoryFromDb = await _transactionCategoryRepository.GetByIdAsync(id, isTracking: false);
            if (transactionCategoryFromDb == null)
            {
                return new ServiceResponse<TransactionCategoryResponseDto>
                {
                    Success = false,
                    Message = "Transaction category not found",
                    Data = null
                };
            }
            transactionCategoryFromDb.UpdateEntity(transactionCategoryUpdateDto);


            await _transactionCategoryRepository.UpdateAsync(transactionCategoryFromDb);
            return new ServiceResponse<TransactionCategoryResponseDto>
            {
                Success = true,
                Message = "Transaction category updated",
                Data = transactionCategoryFromDb.ToResponseDto()
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

     

   

       
    }
}

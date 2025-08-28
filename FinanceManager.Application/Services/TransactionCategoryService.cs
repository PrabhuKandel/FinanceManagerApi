using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Exceptions;
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
                throw new NotFoundException(" Transaction categories  not found");
            }

            var transactionCategoriesDtos = transactionCategories.ToResponseDtoList();


            return new ServiceResponse<IEnumerable<TransactionCategoryResponseDto>>
                {

                  
                    Message = "Fetched Successfully",
                    Data = transactionCategoriesDtos,
                   
                };
            
        }
        public async Task<ServiceResponse<TransactionCategoryResponseDto>> GetTransactionCategoryByIdAsync(Guid id)
        {
            var transactionCategory = await _transactionCategoryRepository.GetByIdAsync(id);
            if (transactionCategory == null)
            {
                throw new NotFoundException("Transaction category not found");
            }

            var transcationCategoryDto = transactionCategory.ToResponseDto();


            return new ServiceResponse<TransactionCategoryResponseDto>
            {

              
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
           
                Message = "New transaction category added",
                Data = entity.ToResponseDto()
            };
        }

        public async Task<ServiceResponse<TransactionCategoryResponseDto>> UpdateTransactionCategoryAsync(Guid id, TransactionCategoryUpdateDto transactionCategoryUpdateDto)
        {
            var transactionCategoryFromDb = await _transactionCategoryRepository.GetByIdAsync(id, isTracking: false);
            if (transactionCategoryFromDb == null)
            {
                throw new NotFoundException("Transaction category doesn't exist");
            }
            transactionCategoryFromDb.UpdateEntity(transactionCategoryUpdateDto);


            await _transactionCategoryRepository.UpdateAsync(transactionCategoryFromDb);
            return new ServiceResponse<TransactionCategoryResponseDto>
            {
          
                Message = "Transaction category updated",
                Data = transactionCategoryFromDb.ToResponseDto()
            };
        }

        public async Task<ServiceResponse<String>> DeleteTransactionCategoryAsync(Guid id)

        {
            var transactionCategoryFromDb = await _transactionCategoryRepository.GetByIdAsync(id);
            if(transactionCategoryFromDb==null)

            {
                throw new NotFoundException("Transaction category  doesn't exist");
            }
   
            
          
            await _transactionCategoryRepository.DeleteAsync(transactionCategoryFromDb);

            return new ServiceResponse<String>
            {

                Message = "Transaction category deleted",
                Data = null
            };

        }

     

   

       
    }
}

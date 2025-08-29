using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Interfaces.Repositories;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Mapping;

using FluentValidation;

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


            var transactionCategoriesDtos = transactionCategories?.ToResponseDtoList();


            return new ServiceResponse<IEnumerable<TransactionCategoryResponseDto>>
            {
                

                Data = transactionCategoriesDtos,
                Message = transactionCategoriesDtos.Any()
                 ? "Transaction categories retrieved successfully"
             : "  No Transaction categories "

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

                Data = transcationCategoryDto,
                Message = "Transaction category  retrieved successfully"
           

            };


        }
        public async Task<ServiceResponse<TransactionCategoryResponseDto>> AddTransactionCategoryAsync(TransactionCategoryCreateDto transactionCategoryCreateDto)
        {
            if (transactionCategoryCreateDto == null)
                throw new CustomValidationException(new[] { "Fields cannot be empty" });


            //Default validation is used for now
            //var validationResult = _createValidator.Validate(transactionCategoryCreateDto);

            //if (!validationResult.IsValid)
            //{
                
            //    throw new CustomValidationException(validationResult.Errors.Select(e => e.ErrorMessage));
            //}

            if (await _transactionCategoryRepository.ExistsByNameAsync(transactionCategoryCreateDto.Name))
                throw new CustomValidationException(new[] { "Transaction category name already exists." });

            var entity = transactionCategoryCreateDto.ToEntity();
           
            await   _transactionCategoryRepository.AddAsync(entity);


            return new ServiceResponse<TransactionCategoryResponseDto>
            {
           
                Message = "New transaction category added",
                Data = entity.ToResponseDto()
            };
        }

        public async Task<ServiceResponse<TransactionCategoryResponseDto>> UpdateTransactionCategoryAsync(Guid id,TransactionCategoryUpdateDto transactionCategoryUpdateDto)
        {
            var transactionCategoryFromDb = await _transactionCategoryRepository.GetByIdAsync(id);
            if (transactionCategoryFromDb == null)
            {
                throw new NotFoundException("Transaction category doesn't exist");
            }
            
            //var validationResult = _updateValidator.Validate(transactionCategoryUpdateDto);

            //if (!validationResult.IsValid)
            //{
                
            //    throw new CustomValidationException(validationResult.Errors.Select(e => e.ErrorMessage));
            //}

            
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
             
            };

        }

     

   

       
    }
}

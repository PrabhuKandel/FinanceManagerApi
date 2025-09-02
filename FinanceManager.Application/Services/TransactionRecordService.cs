using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Interfaces.Repositories;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Mapping;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Services
{
    public class TransactionRecordService : ITransactionRecordService
    {
        public readonly ITransactionRecordRepository _transactionRecordRepository;
        public readonly ITransactionCategoryRepository _transactionCategoryRepository;
        public readonly IPaymentMethodRepository _paymentMethodRepository;
        public readonly IUserContext _userContext;
        public TransactionRecordService(
            ITransactionRecordRepository transactionRecordRepository,
            ITransactionCategoryRepository transactionCategoryRepository,
            IPaymentMethodRepository paymentMethodRepository,
            IUserContext userContext

            )
        {
            _transactionRecordRepository = transactionRecordRepository;
            _transactionCategoryRepository = transactionCategoryRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _userContext = userContext;
        }
        public async Task<ServiceResponse<IEnumerable<TransactionRecordResponseDto>>> GetAllTransactionRecordsAsync()
        {
            var transactionRecordsFromDb = await _transactionRecordRepository.GetAllAsync(_userContext.UserId);
            if (!transactionRecordsFromDb.Any())
            {
                throw new NotFoundException("Transaction record doesn't exist");
            }



            var transactionRecordsDtos = transactionRecordsFromDb.ToResponseDtoList();


            return new ServiceResponse<IEnumerable<TransactionRecordResponseDto>>
            {


                Data = transactionRecordsDtos,
                Message ="Transaction records retrieved successfully"

            };

        }
        public async Task<ServiceResponse<TransactionRecordResponseDto>> GetTransactionRecordByIdAsync(Guid id)
        {

            var transactionRecord = await _transactionRecordRepository.GetByIdAsync(id, _userContext.UserId);
            if (transactionRecord == null)
            {
                throw new NotFoundException("Transaction record not found");
            }

            var transactionRecordDto = transactionRecord.ToResponseDto();



            return new ServiceResponse<TransactionRecordResponseDto>
            {

                Data = transactionRecordDto,
                Message = "Transaction record  retrieved successfully"


            };
        }
        public async  Task<ServiceResponse<TransactionRecordResponseDto>> AddTransactionRecordAsync(TransactionRecordCreateDto transactionRecordCreateDto)
        {
            

            if (!await _transactionCategoryRepository.ExistByIdAsync(transactionRecordCreateDto.TransactionCategoryId))
                throw new CustomValidationException(new[] { "Invalid Transaction Category" });

            if (!await _paymentMethodRepository.ExistByIdAsync(transactionRecordCreateDto.PaymentMethodId))
                throw new CustomValidationException(new[] { "Invalid Payment Method" });

            var entity = transactionRecordCreateDto.ToEntity();
            entity.CreatedByApplicationUserId = _userContext.UserId;
            entity.UpdatedByApplicationUserId = _userContext.UserId;

            var savedEntity = await _transactionRecordRepository.AddAsync(entity);


            return new ServiceResponse<TransactionRecordResponseDto>
            {

                Message = "New transaction category added",
                Data = savedEntity.ToResponseDto()
            };

        }
        public async Task<ServiceResponse<TransactionRecordResponseDto>> UpdateTransactionRecordAsync(Guid id, TransactionRecordUpdateDto transactionRecordUpdateDto)
        {
            var transactionRecordFromDb = await _transactionRecordRepository.GetByIdAsync(id, _userContext.UserId);
            if (transactionRecordFromDb == null)
            {
                throw new NotFoundException("Transaction record doesn't exist");
            }

            transactionRecordFromDb.UpdatedByApplicationUserId = _userContext.UserId;


            transactionRecordFromDb.UpdateEntity(transactionRecordUpdateDto);

            await _transactionRecordRepository.UpdateAsync(transactionRecordFromDb);
            return new ServiceResponse<TransactionRecordResponseDto>
            {

                Message = "Transaction category updated",
                Data = transactionRecordFromDb.ToResponseDto()
            };
        }
        public async Task<ServiceResponse<string>> DeleteTransactionRecordAsync(Guid id)
        {
            var transactionRecordFromDb = await _transactionRecordRepository.GetByIdAsync(id, _userContext.UserId);
            if (transactionRecordFromDb == null)

            {
                throw new NotFoundException("Transaction record  doesn't exist");
            }



            await _transactionRecordRepository.DeleteAsync(transactionRecordFromDb);

            return new ServiceResponse<String>
            {

                Message = "Transaction record deleted",

            };
        }

        public async Task<ServiceResponse<IEnumerable<TransactionRecordResponseDto>>> FilterTransactionRecordsAsync(decimal? minAmount, decimal? maxAmount, Guid? transacionCategory, Guid? paymentMethod, DateTime? transactionDate)
        {
            var transactionRecordsFromDb = await _transactionRecordRepository.FilterTransactionRecordsAsync(_userContext.UserId, minAmount, maxAmount, transacionCategory, paymentMethod, transactionDate);
            if (!transactionRecordsFromDb.Any())
            {
                throw new NotFoundException("Transaction record not found");
            }

            var transactionRecordsDtos = transactionRecordsFromDb.ToResponseDtoList();



            return new ServiceResponse<IEnumerable<TransactionRecordResponseDto>>
            {


                Data = transactionRecordsDtos,
                Message ="Transaction records retrieved successfully" 

            };
        }
    }
}

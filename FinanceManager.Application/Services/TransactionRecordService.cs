using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Interfaces.Repositories;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Mapping;
using FinanceManager.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Services
{
    public class TransactionRecordService : ITransactionRecordService
    {
        public readonly ITransactionRecordRepository _transactionRecordRepository;
        public readonly ITransactionCategoryRepository _transactionCategoryRepository;
        public readonly IPaymentMethodRepository _paymentMethodRepository;
        public readonly IUserContext _userContext;
        public readonly UserManager<ApplicationUser> _userManager;
        public TransactionRecordService(
            ITransactionRecordRepository transactionRecordRepository,
            ITransactionCategoryRepository transactionCategoryRepository,
            IPaymentMethodRepository paymentMethodRepository,
            IUserContext userContext,
            UserManager<ApplicationUser> userManager

            )
        {
            _transactionRecordRepository = transactionRecordRepository;
            _transactionCategoryRepository = transactionCategoryRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _userContext = userContext;
            _userManager = userManager;
            _userManager = userManager;
        }
        public async Task<ServiceResponse<IEnumerable<TransactionRecordResponseDto>>> GetAllTransactionRecordsAsync()
        {
            var transactionRecordsFromDb = await _transactionRecordRepository.GetAllAsync();
            if (!transactionRecordsFromDb.Any())
            {
                throw new NotFoundException("Transaction record doesn't exist");
            }

            // Filter for non-admin users
            if (!await IsUserAdmin(_userContext.UserId))
            {
                transactionRecordsFromDb = transactionRecordsFromDb
                    .Where(t => t.CreatedByApplicationUserId == _userContext.UserId)
                    .ToList();
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

            var transactionRecord = await _transactionRecordRepository.GetByIdAsync(id);
            if (transactionRecord == null)
            {
                throw new NotFoundException("Transaction record not found");
            }
            // Filter for non-admin users
            if (!await IsUserAdmin(_userContext.UserId))
            {
                if (transactionRecord.CreatedByApplicationUserId != _userContext.UserId)
                {
                    throw new UnauthorizedAccessException("You can't access this record.");
                }
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
            var transactionRecordFromDb = await _transactionRecordRepository.GetByIdAsync(id);
            if (transactionRecordFromDb == null)
            {
                throw new NotFoundException("Transaction record doesn't exist");
            }
            // admin can update all but user can update theirs only
            if (!await IsUserAdmin(_userContext.UserId))
            {
                if (transactionRecordFromDb.CreatedByApplicationUserId != _userContext.UserId)
                {
                    throw new UnauthorizedAccessException("You can't access this record.");
                }
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
            var transactionRecordFromDb = await _transactionRecordRepository.GetByIdAsync(id);
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
            var transactionRecordsFromDb = await _transactionRecordRepository.FilterTransactionRecordsAsync( minAmount, maxAmount, transacionCategory, paymentMethod, transactionDate);
            if (!transactionRecordsFromDb.Any())
            {
                throw new NotFoundException("Transaction record not found");
            }
            // Filter for non-admin users
            if (!await IsUserAdmin(_userContext.UserId))
            {
                transactionRecordsFromDb = transactionRecordsFromDb
                    .Where(t => t.CreatedByApplicationUserId == _userContext.UserId)
                    .ToList();
            }
            var transactionRecordsDtos = transactionRecordsFromDb.ToResponseDtoList();



            return new ServiceResponse<IEnumerable<TransactionRecordResponseDto>>
            {


                Data = transactionRecordsDtos,
                Message ="Transaction records retrieved successfully" 

            };
        }

        private async Task<bool> IsUserAdmin(String userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;
            var roles = await _userManager.GetRolesAsync(user);
            return roles.Contains(RoleConstants.Admin);
        }
    }
}

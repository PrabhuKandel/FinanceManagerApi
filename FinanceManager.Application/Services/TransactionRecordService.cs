using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Interfaces.Repositories;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Mapping;
using FinanceManager.Application.Validators.TransactionCategoryValidator;
using FinanceManager.Application.Validators.TransactionRecordValidator;
using FinanceManager.Domain.Models;
using FluentValidation;
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
        private readonly IValidator<TransactionRecordCreateDto> _createValidator;
        private readonly IValidator<TransactionRecordUpdateDto> _updateValidator;
        public TransactionRecordService(
            ITransactionRecordRepository transactionRecordRepository,
            ITransactionCategoryRepository transactionCategoryRepository,
            IPaymentMethodRepository paymentMethodRepository,
            IUserContext userContext,
            UserManager<ApplicationUser> userManager,
            IValidator<TransactionRecordCreateDto> createValidator,
            IValidator<TransactionRecordUpdateDto> updateValidator


            )
        {
            _transactionRecordRepository = transactionRecordRepository;
            _transactionCategoryRepository = transactionCategoryRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _userContext = userContext;
            _userManager = userManager;
            _userManager = userManager;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        public async Task<ServiceResponse<IEnumerable<TransactionRecordResponseDto>>> GetAllTransactionRecordsAsync()
        {
            var transactionRecordsFromDb = await _transactionRecordRepository.GetAllAsync();
            if (!transactionRecordsFromDb.Any())
            {
                throw new NotFoundException("Transaction record doesn't exist");
            }
            // Check admin status once
            var isAdmin = await IsUserAdmin(_userContext.UserId);
            // Filter for non-admin users
            if (!isAdmin)
            {
                transactionRecordsFromDb = transactionRecordsFromDb
                    .Where(t => t.CreatedByApplicationUserId == _userContext.UserId)
                    .ToList();
            }

            var transactionRecordsDtos = transactionRecordsFromDb.ToResponseDtoList(isAdmin);




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
            var isAdmin = await IsUserAdmin(_userContext.UserId);
            // Filter for non-admin users
            if (!isAdmin)
            {
                if (transactionRecord.CreatedByApplicationUserId != _userContext.UserId)
                {
                    throw new UnauthorizedAccessException("You can't access this record.");
                }
            }
            var transactionRecordDto = transactionRecord.ToResponseDto(isAdmin);



            return new ServiceResponse<TransactionRecordResponseDto>
            {

                Data = transactionRecordDto,
                Message = "Transaction record  retrieved successfully"


            };
        }
        public async  Task<ServiceResponse<TransactionRecordResponseDto>> AddTransactionRecordAsync(TransactionRecordCreateDto transactionRecordCreateDto)
        {

            var validationResult = _createValidator.Validate(transactionRecordCreateDto);

            if (!validationResult.IsValid)
            {

                throw new CustomValidationException(validationResult.Errors.Select(e => e.ErrorMessage));
            }



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
            var validationResult = _updateValidator.Validate(transactionRecordUpdateDto);

            if (!validationResult.IsValid)
            {

                throw new CustomValidationException(validationResult.Errors.Select(e => e.ErrorMessage));
            }

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

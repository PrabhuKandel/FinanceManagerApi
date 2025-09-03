using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Exceptions;
using FinanceManager.Application.Interfaces.Repositories;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Mapping;
using FinanceManager.Application.Validators.PaymentMethodValidator;
using FluentValidation;

namespace FinanceManager.Application.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IValidator<PaymentMethodCreateDto> _createValidator;
        private readonly IValidator<PaymentMethodUpdateDto> _updateValidator;
  

        public PaymentMethodService(
            IPaymentMethodRepository paymentMethodRepository,
            IValidator<PaymentMethodCreateDto> createValidator,
             IValidator<PaymentMethodUpdateDto> updateValidator
          )
        {
            _paymentMethodRepository = paymentMethodRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        
        }
        public async Task<ServiceResponse<IEnumerable<PaymentMethodResponseDto>>> GetAllPaymentMethodsAsync()
        {
            var paymentMethods = await _paymentMethodRepository.GetAllAsync();


            var paymentMethodDtos = paymentMethods?.ToResponseDtoList();


            return new ServiceResponse<IEnumerable<PaymentMethodResponseDto>>
            {


                Data = paymentMethodDtos,
                Message = paymentMethodDtos.Any()
                 ? "Payment methods retrieved successfully"
             : "  No payment methods "

            };
        }
        public async Task<ServiceResponse<PaymentMethodResponseDto>> GetPaymentMethodByIdAsync(Guid id)
        {
            var paymentMethod = await _paymentMethodRepository.GetByIdAsync(id);
            if (paymentMethod == null)
            {
                throw new NotFoundException("Payment Method not found");
            }

            var paymentMethodDto = paymentMethod.ToResponseDto();



            return new ServiceResponse<PaymentMethodResponseDto>
            {

                Data = paymentMethodDto,
                Message = "Payment Method retrieved successfully"


            };
        }

        public async Task<ServiceResponse<PaymentMethodResponseDto>> AddPaymentMethodAsync(PaymentMethodCreateDto paymentMethodCreateDto)
        {


            var validationResult = _createValidator.Validate(paymentMethodCreateDto);

            if (!validationResult.IsValid)
            {

                throw new CustomValidationException(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            if (await _paymentMethodRepository.ExistsByNameAsync(paymentMethodCreateDto.Name))
                throw new CustomValidationException(new[] { "Payment method with this name already exists." });

            var entity = paymentMethodCreateDto.ToEntity();

            await _paymentMethodRepository.AddAsync(entity);


            return new ServiceResponse<PaymentMethodResponseDto>
            {

                Message = "New payment method added",
                Data = entity.ToResponseDto()
            };
        }
        public async Task<ServiceResponse<PaymentMethodResponseDto>> UpdatePaymentMethodAsync(Guid id, PaymentMethodUpdateDto paymentMethodUpdateDto)
        {
            var validationResult = _updateValidator.Validate(paymentMethodUpdateDto);

            if (!validationResult.IsValid)
            {

                throw new CustomValidationException(validationResult.Errors.Select(e => e.ErrorMessage));
            }
            var paymentMethodFromDb = await _paymentMethodRepository.GetByIdAsync(id);
            if (paymentMethodFromDb == null)
            {
                throw new NotFoundException("Payment Method doesn't exist");
            }



            paymentMethodFromDb.UpdateEntity(paymentMethodUpdateDto);

            await _paymentMethodRepository.UpdateAsync(paymentMethodFromDb);
            return new ServiceResponse<PaymentMethodResponseDto>
            {

                Message = "Payment method updated",
                Data = paymentMethodFromDb.ToResponseDto()
            };
        }

        public async Task<ServiceResponse<string>> DeletePaymentMethodAsync(Guid id)
        {
            var paymentMethodFromDb = await _paymentMethodRepository.GetByIdAsync(id);
            if (paymentMethodFromDb == null)
            {
                throw new NotFoundException("Payment Method doesn't exist");
            }
          



            await _paymentMethodRepository.DeleteAsync(paymentMethodFromDb);

            return new ServiceResponse<String>
            {

                Message = "Payment method deleted",

            };
        }

      
    

       
    }
}

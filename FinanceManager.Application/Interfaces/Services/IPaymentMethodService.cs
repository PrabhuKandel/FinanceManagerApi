using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Common;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Domain.Models;

namespace FinanceManager.Application.Interfaces.Services
{
    public interface IPaymentMethodService
    {
        Task<ServiceResponse<IEnumerable<PaymentMethodResponseDto>>> GetAllPaymentMethodsAsync();
        Task<ServiceResponse<PaymentMethodResponseDto>> GetPaymentMethodByIdAsync(Guid id);
        Task<ServiceResponse<PaymentMethodResponseDto>> AddPaymentMethodAsync(PaymentMethodCreateDto paymentMethodCreateDto);
        Task<ServiceResponse<PaymentMethodResponseDto>> UpdatePaymentMethodAsync(Guid id, PaymentMethodUpdateDto paymentMethodUpdateDto);
        Task<ServiceResponse<String>> DeletePaymentMethodAsync(Guid id);


    }
}

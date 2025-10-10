using FinanceManager.Application.Dtos.TransactionPayment;
using FinanceManager.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Validators.TransactionPaymentValidator
{
    public class TransactionPaymentUpdateDtoValidator : AbstractValidator<TransactionPaymentDto>
    {
        public TransactionPaymentUpdateDtoValidator(IApplicationDbContext _context)
        {
    
        }
    }
}

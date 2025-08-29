using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Dtos.TransactionCategory;
using FluentValidation;

namespace FinanceManager.Application.Validators.TransactionCategoryValidator
{
    public class TransactionCategoryUpdateDtoValidator:AbstractValidator<TransactionCategoryUpdateDto>
    {
        public TransactionCategoryUpdateDtoValidator()
        {

        

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(c => c.Description)
                .MaximumLength(250).WithMessage("Description cannot exceed 250 characters.");

            RuleFor(c => c.Type)
                .IsInEnum().WithMessage("Type is invalid.");    
        }
    }
}

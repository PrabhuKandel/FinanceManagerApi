using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Validators.TransactionCategoryValidator
{
    public class TransactionCategoryBaseDtoValidator<T>:AbstractValidator<T> where T : TransactionCategoryBaseDto
    {

        public TransactionCategoryBaseDtoValidator(IApplicationDbContext _context)
        {
            RuleFor(c => c.Name)
         .NotEmpty().WithMessage("Name is required.")
          .MustAsync(async (name, cancellation) =>
          {
              var exists = await _context.TransactionCategories.AnyAsync(pm => pm.Name == name);
              return !exists;// true = valid, false = invalid
          })
         .WithMessage("Transaction category with this  name already exists");

            RuleFor(c => c.Description)
                .MaximumLength(250).WithMessage("Description cannot exceed 250 characters.");

            RuleFor(c => c.Type)
                .IsInEnum().WithMessage("Type is invalid.");
        }
    }
}

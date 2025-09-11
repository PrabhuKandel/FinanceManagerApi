using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Application.Features.TransactionCategories.Commands;
using FinanceManager.Infrastructure.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.FeaturesStoredProcedure.TransactionCategory.Commands.CreateTransactionCategory
{
    public class CreateTransactionCategorySpCommandValidator : AbstractValidator<CreateTransactionCategorySpCommand>
    {

        public CreateTransactionCategorySpCommandValidator(ApplicationDbContext _context )
        {
             RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MustAsync(async (name, cancellation) =>
            {
                var exists = await _context.TransactionCategories.AnyAsync(tc => tc.Name == name);
                return !exists;// true = valid, false = invalid
            })
            .WithMessage("Transaction category with this  name already exists");
            
            RuleFor(c => c.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

             RuleFor(c => c.Type)
            .IsInEnum().WithMessage("Type is invalid.");
        }

    }
}



using FinanceManager.Application.Interfaces;
using FinanceManager.Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
namespace FinanceManager.Application.Features.Budgets.Commands.Create
{
    public class CreateBudgetCommandValidator:AbstractValidator<CreateBudgetCommand>
    {
        public CreateBudgetCommandValidator(IApplicationDbContext _context)
        {
            RuleFor(x => x.TransactionCategoryId)
             .NotEmpty().WithMessage("Transaction category is required.")
             .MustAsync(async (transactionCategoryId, cancellation) =>
             {
                 var exists = await _context.TransactionCategories.AnyAsync(c => c.Id == transactionCategoryId);
                 return exists;// true = valid, false = invalid //valid only if category exists
             })
             .WithMessage("Invalid transaction category");


            RuleFor(x => x.TransactionCategoryId)
                .MustAsync(async (id, cancellation) =>
                {
                    var type = await _context.TransactionCategories
                        .Where(c => c.Id == id)
                        .Select(c => c.Type)
                        .FirstOrDefaultAsync(cancellation);

                    return type == CategoryType.Expense;
                })
                .WithMessage("Budget can only be created for expense categories.");


            RuleFor(x => x.Amount)
            .NotEmpty().WithMessage("Amount is required")
            .GreaterThan(0m).WithMessage("Amount must be greater than 0");


            RuleFor(x => x.PeriodType)
                .IsInEnum().WithMessage("Invalid Period Type");


            RuleFor(x => x.SelectedPeriod)
                .NotEmpty().WithMessage("Selected period is required.")
                .Must(BeValidSelectedPeriod)
                .WithMessage("Selected period format does not match the selected period type.");

            RuleFor(x=>x.IsActive)
                .NotNull().WithMessage("IsActive  must be specified.");

        }
        private bool BeValidSelectedPeriod(CreateBudgetCommand command, string selectedPeriod)
        {
            return command.PeriodType switch
            {
                Domain.Enums.PeriodType.Daily => DateTime.TryParseExact(
                    selectedPeriod, "yyyy-MM-dd", null,
                    System.Globalization.DateTimeStyles.None, out _),

                Domain.Enums.PeriodType.Weekly => Regex.IsMatch(
                    selectedPeriod, @"^\d{4}-W\d{2}$"), // e.g., 2025-W42

                Domain.Enums.PeriodType.Monthly => Regex.IsMatch(
                    selectedPeriod, @"^\d{4}-(0[1-9]|1[0-2])$"), // e.g., 2025-10

                Domain.Enums.PeriodType.Yearly => Regex.IsMatch(
                    selectedPeriod, @"^\d{4}$"), // e.g., 2025

                _ => false
            };
        }
    }
}

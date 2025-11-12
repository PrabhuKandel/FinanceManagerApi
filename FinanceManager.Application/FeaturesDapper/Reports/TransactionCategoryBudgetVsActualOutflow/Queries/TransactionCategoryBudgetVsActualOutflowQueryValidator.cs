using FinanceManager.Application.Interfaces;
using FinanceManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using System.Text.RegularExpressions;

namespace FinanceManager.Application.FeaturesDapper.Reports.TransactionCategoryBudgetVsActualOutflow.Queries
{
    public class TransactionCategoryBudgetVsActualOutflowQueryValidator:AbstractValidator<TransactionCategoryBudgetVsActualOutflowQuery>
    {
        public TransactionCategoryBudgetVsActualOutflowQueryValidator(IApplicationDbContext _context)
        {
            RuleFor(x => x.TransactionCategoryId)
         .MustAsync(async (id, cancellation) =>
         {
             return await _context.TransactionCategories
           .AnyAsync(c => c.Id == id && c.Type == CategoryType.Expense, cancellation);
         })
           .When(x => x.TransactionCategoryId.HasValue)
         .WithMessage("Invalid Transaction Category");


            RuleFor(x => x.PeriodStart)
        .NotEmpty().WithMessage(" Period start is required.")
        .Must(BeValidSelectedPeriod)
        .WithMessage("Period start format does not match the selected period type.");

            RuleFor(x => x.PeriodEnd)
            .NotEmpty().WithMessage(" Period end is required.")
            .Must(BeValidSelectedPeriod)
            .WithMessage("Period end format does not match the selected period type.");



        }
        private bool BeValidSelectedPeriod(TransactionCategoryBudgetVsActualOutflowQuery query, string selectedPeriod)
        {
            return query.PeriodType switch
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


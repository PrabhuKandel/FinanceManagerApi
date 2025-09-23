

using FinanceManager.Application.Features.Report.Commands.TransactionRecordSummaryByIncomeExpense;
using FinanceManager.Application.FeaturesDapper.Reports.Queries.TransactionRecordSummaryByTransactionCategory;
using FinanceManager.Application.Interfaces;
using FluentValidation;

namespace FinanceManager.Application.Features.Report.Commands.TransactionRecordSummaryByCategoryType
{
    public class TransactionRecordSummaryByCategoryTypeCommandValidator : AbstractValidator<TransactionRecordSummaryByCategoryTypeCommand>
    {


        public TransactionRecordSummaryByCategoryTypeCommandValidator(IApplicationDbContext context)
        {

            RuleFor(x => x)
                .Must(x => x.From <= x.To)
                .WithMessage("FromDate must be earlier than or equal to ToDate.")
                .When(x => x.From.HasValue && x.To.HasValue);


  
            RuleFor(x => x.CategoryType)
                .IsInEnum()
                .WithMessage("CategoryType must be a valid enum value.")
                .When(x => x.CategoryType.HasValue);



        }
    }
}

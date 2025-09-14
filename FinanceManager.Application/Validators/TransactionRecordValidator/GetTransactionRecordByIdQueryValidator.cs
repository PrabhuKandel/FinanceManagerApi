using FinanceManager.Application.Features.TransactionRecords.Queries;
using FinanceManager.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Validators.TransactionRecordValidator
{
    public class GetTransactionRecordByIdQueryValidator : AbstractValidator<GetTransactionRecordByIdQuery>
    {
        public GetTransactionRecordByIdQueryValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.")
                .MustAsync(async (id, cancellation) =>
                {
                    return await context.TransactionRecords.AnyAsync(pm => pm.Id == id, cancellation);
                })
                .WithMessage("Transaction record not found.");
        }
    }
}

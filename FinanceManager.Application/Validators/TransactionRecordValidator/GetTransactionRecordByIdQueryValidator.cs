using FinanceManager.Application.Features.TransactionRecords.Queries;
using FinanceManager.Infrastructure.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Validators.TransactionRecordValidator
{
    public class GetTransactionRecordByIdQueryValidator : AbstractValidator<GetTransactionRecordByIdQuery>
    {
        public GetTransactionRecordByIdQueryValidator(ApplicationDbContext context)
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

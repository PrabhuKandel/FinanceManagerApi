
using FinanceManager.Application.Interfaces;
using FinanceManager.Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.TransactionRecords.Commands.PatchApprovalStatus
{
    public class PatchTransactionRecordApprovalStatusCommandValidator:AbstractValidator<PatchTransactionRecordApprovalStatusCommand>
    {
        public PatchTransactionRecordApprovalStatusCommandValidator(IApplicationDbContext _context)
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .MustAsync(async (id, cancellation) =>
                {
                    var exists = await _context.TransactionRecords.AnyAsync(c => c.Id == id);
                    return exists;// true = valid, false = invalid //valid only if category exists
                })
                .WithMessage("Invalid transaction record");



            RuleFor(c => c.ApprovalStatus)
                .IsInEnum().WithMessage("ApprovalStatus is invalid");



            RuleFor(c => c)
                .MustAsync(async (command, cancellation) =>
                {
                    var record = await _context.TransactionRecords
                        .FirstOrDefaultAsync(r => r.Id == command.Id, cancellation);
                    if (record == null) return false;

                    // Only allow changing status if current status is Pending
                    return record.ApprovalStatus == TransactionRecordApprovalStatus.Pending;
                })
                .WithMessage("Only pending transactions can be approved or cancelled.")
                 .OverridePropertyName(nameof(PatchTransactionRecordApprovalStatusCommand.ApprovalStatus));

        }
    }
}

using FinanceManager.Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Application.Features.TransactionRecords.Commands.DeleteAttachment
{
    public class DeleteTransactionRecordAttachmentCommandValidator:AbstractValidator<DeleteTransactionRecordAttachmentCommand>
    {
        private readonly IApplicationDbContext context;
        public DeleteTransactionRecordAttachmentCommandValidator(IApplicationDbContext _context)
        {
            context = _context;

            RuleFor(x => x.TransactionRecordId)
                .NotEmpty().WithMessage("TransactionRecordId is required.")
                .MustAsync(TransactionExists).WithMessage("Transaction record does not exist.")
                    .DependentRules(() =>
                    {
                        // Only validate attachments if the TransactionRecordId exists
                        RuleFor(x => x.TransactionAttachmentIds)
                            .NotEmpty().WithMessage("At least one attachment ID must be provided.");

                        RuleFor(x => x)
                            .MustAsync(async (command, ct) =>
                            {
                                if (command.TransactionAttachmentIds == null || !command.TransactionAttachmentIds.Any())
                                    return false;

                                var count = await _context.TransactionAttachments
                                    .Where(a => command.TransactionAttachmentIds.Contains(a.Id)
                                                && a.TransactionRecordId == command.TransactionRecordId)
                                    .CountAsync(ct);

                                return count == command.TransactionAttachmentIds.Count;
                            })
                            .WithMessage("One or more attachment IDs are invalid.")
                            .WithName(nameof(DeleteTransactionRecordAttachmentCommand.TransactionAttachmentIds));
                    });

            RuleFor(x => x.TransactionAttachmentIds)
                .NotEmpty().WithMessage("At least one attachment ID must be provided.");


        }

        private async Task<bool> TransactionExists(Guid transactionId, CancellationToken ct)
        {
            return await context.TransactionRecords.AnyAsync(t => t.Id == transactionId, ct);
        }


    }
}


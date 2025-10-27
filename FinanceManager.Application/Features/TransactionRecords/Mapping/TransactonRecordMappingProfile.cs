using FinanceManager.Application.Features.TransactionCategories.Commands.Create;
using FinanceManager.Application.Features.TransactionRecords.Commands.Create;
using FinanceManager.Application.Features.TransactionRecords.Commands.Update;
using FinanceManager.Application.Features.TransactionRecords.Dtos;
using FinanceManager.Domain.Entities;
using static FinanceManager.Application.Dtos.Shared.SummaryDtos;

namespace FinanceManager.Application.Features.TransactionRecords.Mapping
{
    public static class TransactonRecordMappingProfile
    {
        public static TransactionRecordResponseDto? ToResponseDto(this TransactionRecord entity, bool isAdmin = false)
        {
            if (entity == null) return null;

            var dto = new TransactionRecordResponseDto
            {
                Id = entity.Id,
                TransactionCategory = entity.TransactionCategory.ToSummary(),
                TransactionPayments = entity.TransactionPayments!.Select(tp => tp.ToSummary()).ToList(),
                Amount = entity.Amount,
                Description = entity.Description,
                TransactionDate = entity.TransactionDate,
                ApprovalStatus = entity.ApprovalStatus.ToString(),
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };

            // Only populate for admins
            if (isAdmin)
            {
                dto.CreatedBy = entity.CreatedByApplicationUser?.ToSummary();
                dto.UpdatedBy = entity.UpdatedByApplicationUser?.ToSummary();
                dto.ActionedBy = entity.ActionedByApplicationUser?.ToSummary();
            }

            return dto;


        }
        public static List<TransactionRecordResponseDto> ToResponseDtoList(this IEnumerable<TransactionRecord> entities, bool isAdmin = false)
        {
            return entities?.Select(e => e.ToResponseDto(isAdmin))
                .OfType<TransactionRecordResponseDto>()// filters nulls and makes non-nullable
                .ToList() ?? new List<TransactionRecordResponseDto>();
        }

        public static TransactionRecord ToEntity(this CreateTransactionRecordCommand dto, string UserId)
        {
            return new TransactionRecord
            {
                TransactionCategoryId = dto.TransactionCategoryId,
                Amount = dto.Amount,
                Description = dto.Description,
                TransactionDate = dto.TransactionDate,
                CreatedByApplicationUserId = UserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow

            };
        }




        public static void UpdateEntity(this TransactionRecord entity, UpdateTransactionRecordCommand dto)
        {
            entity.TransactionCategoryId = dto.TransactionCategoryId;
            entity.Amount = dto.Amount;
            entity.Description = dto.Description;
            entity.TransactionDate = dto.TransactionDate;
            entity.UpdatedAt = DateTime.UtcNow;


        }

        private static TransactionCategorySummaryDto? ToSummary(this TransactionCategory? category) =>
        category == null ? null : new TransactionCategorySummaryDto
        {
            Id = category.Id,
            Name = category.Name
        };

        private static ApplicationUserSummaryDto? ToSummary(this ApplicationUser? user) =>
            user == null ? null : new ApplicationUserSummaryDto
            {
                Id = user.Id,
                Email = user.Email!.ToString(),
            };

        private static TransactionPaymentSummaryDto? ToSummary(this TransactionPayment? payment) =>
            payment == null ? null : new TransactionPaymentSummaryDto
            {
                PaymentMethodId = payment.PaymentMethodId,
                Name = payment.PaymentMethod!.Name,
                Amount = payment.Amount
            };
    }
}

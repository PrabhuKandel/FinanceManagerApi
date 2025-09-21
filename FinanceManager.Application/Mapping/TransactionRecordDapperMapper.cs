using Ardalis.GuardClauses;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Domain.Entities;
using static FinanceManager.Application.Dtos.Shared.SummaryDtos;

namespace FinanceManager.Application.Mapping
{
    public static class TransactionRecordDapperMapper
    {
        public static TransactionRecordResponseDto ToResponseDtoFromDapper(this TransactionRecordDapperResult dapperResult, bool isAdmin = false)
        {
            Guard.Against.Null(dapperResult, nameof(dapperResult));
            return new TransactionRecordResponseDto
            {
                Id = dapperResult.TransactionRecordId,
                Amount = dapperResult.Amount,
                Description = dapperResult.Description,
                TransactionDate = dapperResult.TransactionDate,
                CreatedAt = dapperResult.CreatedAt,
                UpdatedAt = dapperResult.UpdatedAt,
                TransactionCategory = new TransactionCategorySummaryDto
                {
                    Id = dapperResult.TransactionCategoryId,
                    Name = dapperResult.TransactionCategoryName
                },
                CreatedBy = new ApplicationUserSummaryDto
                {
                    Id = dapperResult.CreatedByUserId,
                    Email = dapperResult.CreatedByFirstName
                },
                UpdatedBy = new ApplicationUserSummaryDto
                {
                    Id = dapperResult.UpdatedByUserId,
                    Email = dapperResult.UpdatedByFirstName
                }
            };
        }
        public static List<TransactionRecordResponseDto> ToResponseDtoListFromDapper(this IEnumerable<TransactionRecordDapperResult> dapperResult, bool isAdmin = false)
        {
            return dapperResult?.Select(e => e.ToResponseDtoFromDapper(isAdmin))
                .OfType<TransactionRecordResponseDto>()// filters nulls and makes non-nullable
                .ToList() ?? new List<TransactionRecordResponseDto>();
        }

    }
}

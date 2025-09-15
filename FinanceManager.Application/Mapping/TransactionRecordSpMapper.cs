using Ardalis.GuardClauses;
using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Domain.Entities;

namespace FinanceManager.Application.Mapping
{
    public static class TransactionRecordSpMapper
    {
        public static TransactionRecordResponseDto ToResponseDtoFromSp(this TransactionRecordSpResult spResult, bool isAdmin=false)
        {
            Guard.Against.Null(spResult, nameof(spResult));
            return new TransactionRecordResponseDto
            {
                Id = spResult.TransactionRecordId,
                Amount = spResult.Amount,
                Description = spResult.Description,
                TransactionDate = spResult.TransactionDate,
                CreatedAt = spResult.CreatedAt,
                UpdatedAt = spResult.UpdatedAt,
                TransactionCategory = new EntitySummaryDto
                {
                    Id = spResult.TransactionCategoryId,
                    Name = spResult.TransactionCategoryName
                },
                PaymentMethod = new EntitySummaryDto
                {
                    Id = spResult.PaymentMethodId,
                    Name = spResult.PaymentMethodName
                },
                CreatedBy = new ApplicationUserSummaryDto
                {
                    Id = spResult.CreatedByUserId,
                    FirstName = spResult.CreatedByFirstName
                },
                UpdatedBy =  new ApplicationUserSummaryDto
                {
                    Id = spResult.UpdatedByUserId,
                    FirstName = spResult.UpdatedByFirstName
                } 
            };
        }

    }
}

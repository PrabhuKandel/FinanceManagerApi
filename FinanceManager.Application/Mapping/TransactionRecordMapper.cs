using FinanceManager.Application.Dtos.TransactionRecord;
using FinanceManager.Domain.Entities;

namespace FinanceManager.Application.Mapping
{
    public  static class TransactionRecordMapper
    {
        public static TransactionRecordResponseDto? ToResponseDto(this TransactionRecord entity, bool isAdmin = false)
        {
            if (entity == null) return null;

             var dto = new TransactionRecordResponseDto
            {
                Id = entity.Id,
                TransactionCategory = new EntitySummaryDto
                {
                    Id = entity.TransactionCategory.Id,
                    Name = entity.TransactionCategory.Name,
             
                },
                PaymentMethod = new EntitySummaryDto
                {
                    Id = entity.PaymentMethod.Id,
                    Name = entity.PaymentMethod.Name,
                  
                },
                Amount = entity.Amount,
                Description = entity.Description,
                TransactionDate = entity.TransactionDate,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
             };
           
            // Only populate for admins
            if (isAdmin)
            {
                dto.CreatedBy = new ApplicationUserSummaryDto
                {
                    Id = entity.CreatedByApplicationUserId,
                    FirstName = entity.CreatedByApplicationUser.FirstName,

                };

                dto.UpdatedBy = new ApplicationUserSummaryDto
                {
                    Id = entity.UpdatedByApplicationUserId,
                    FirstName = entity.UpdatedByApplicationUser.FirstName,

                };
            }

            return dto;

      
        }
        public static List<TransactionRecordResponseDto?>? ToResponseDtoList(this IEnumerable<TransactionRecord> entities, bool isAdmin =false)
        {
            return entities?.Select(e => e.ToResponseDto(isAdmin)).ToList();
        }

        public static TransactionRecord ToEntity(this TransactionRecordCreateDto dto)
        {
            return new TransactionRecord
            {
                TransactionCategoryId = dto.TransactionCategoryId,
                Amount = dto.Amount,
                PaymentMethodId = dto.PaymentMethodId,
                Description = dto.Description,
                TransactionDate = dto.TransactionDate,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow

            };
        }



      
        public static void UpdateEntity(this TransactionRecord entity, TransactionRecordUpdateDto dto)
        {
            entity.TransactionCategoryId = dto.TransactionCategoryId;
            entity.Amount = dto.Amount;
            entity.PaymentMethodId = dto.PaymentMethodId;
            entity.Description = dto.Description;
            entity.TransactionDate = dto.TransactionDate;
            entity.UpdatedAt = DateTime.UtcNow;


        }


    }
}

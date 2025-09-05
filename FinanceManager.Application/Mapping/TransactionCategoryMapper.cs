using FinanceManager.Application.Dtos.TransactionCategory;
using FinanceManager.Domain.Entities;

namespace FinanceManager.Application.Mapping
{
    public  static class TransactionCategoryMapper
    {
        public static TransactionCategoryResponseDto? ToResponseDto(this TransactionCategory entity)
        {
            if (entity == null) return null;

            return new TransactionCategoryResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Type = entity.Type
            };
        }
        public static List<TransactionCategoryResponseDto> ToResponseDtoList(this IEnumerable<TransactionCategory> entities)
        {
            return entities.Select(e => e.ToResponseDto())
                .OfType<TransactionCategoryResponseDto>()
                .ToList();
        }

        public static TransactionCategory ToEntity(this TransactionCategoryCreateDto dto)
        {
            return new TransactionCategory
            {
                Name = dto.Name,
                Description = dto.Description,
                Type = dto.Type
            };
        }



      
        public static void UpdateEntity(this TransactionCategory entity, TransactionCategoryUpdateDto dto)
        {
          
            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.Type = dto.Type;
        }


    }
}

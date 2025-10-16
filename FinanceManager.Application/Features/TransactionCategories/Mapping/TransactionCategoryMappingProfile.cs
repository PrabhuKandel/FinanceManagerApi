

using FinanceManager.Application.Features.TransactionCategories.Commands.Create;
using FinanceManager.Application.Features.TransactionCategories.Commands.Update;
using FinanceManager.Application.Features.TransactionCategories.Dtos;
using FinanceManager.Domain.Entities;

namespace FinanceManager.Application.Features.TransactionCategories.Mapping
{
    public static class TransactionCategoryMappingProfile
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
                .OfType<TransactionCategoryResponseDto>()// filters nulls and makes non-nullable
                .ToList();
        }

        public static TransactionCategory ToEntity(this CreateTransactionCategoryCommand dto)
        {
            return new TransactionCategory
            {
                Name = dto.Name,
                Description = dto.Description,
                Type = dto.Type
            };
        }




        public static void UpdateEntity(this TransactionCategory entity, UpdateTransactionCategoryCommand dto)
        {

            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.Type = dto.Type;
        }
    }
}

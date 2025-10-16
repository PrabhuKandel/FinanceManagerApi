
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Application.Features.PaymentMethods.Commands.Create;
using FinanceManager.Application.Features.PaymentMethods.Commands.Update;
using FinanceManager.Domain.Entities;

namespace FinanceManager.Application.Features.PaymentMethods.Mapping
{
    public static class PaymentMethodMappingProfile
    {
        public static PaymentMethodResponseDto? ToResponseDto(this PaymentMethod entity)
        {

            if (entity == null) return null;
            return new PaymentMethodResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                IsActive = entity.IsActive,
            };
        }
        public static List<PaymentMethodResponseDto> ToResponseDtoList(this IEnumerable<PaymentMethod> entities)
        {
            return entities?.Select(e => e.ToResponseDto())
                 .OfType<PaymentMethodResponseDto>() // filters nulls and makes non-nullable
                .ToList()
                ?? new List<PaymentMethodResponseDto>();
        }


        public static PaymentMethod ToEntity(this CreatePaymentMethodCommand dto)
        {
            return new PaymentMethod
            {
                Name = dto.Name,
                Description = dto.Description,
                IsActive = dto.IsActive,
            };
        }




        public static void UpdateEntity(this PaymentMethod entity, UpdatePaymentMethodCommand dto)
        {

            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.IsActive = dto.IsActive;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Dtos.PaymentMethod;
using FinanceManager.Domain.Models;

namespace FinanceManager.Application.Mapping
{
    public  static class PaymentMethodMapper
    {
        public static PaymentMethodResponseDto ToResponseDto(this PaymentMethod entity)
        {
            if (entity == null) return null;

            return new PaymentMethodResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                IsActive = entity.IsActive??true,
            };
        }
        public static List<PaymentMethodResponseDto> ToResponseDtoList(this IEnumerable<PaymentMethod> entities)
        {
            return entities?.Select(e => e.ToResponseDto()).ToList();
        }

        public static PaymentMethod ToEntity(this PaymentMethodCreateDto dto)
        {
            return new PaymentMethod
            {
                Name = dto.Name,
                Description = dto.Description,
                IsActive = dto.IsActive,
            };
        }
      


      
        public static void UpdateEntity(this PaymentMethod entity, PaymentMethodUpdateDto dto)
        {
          
            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.IsActive = dto.IsActive;
        }


    }
}

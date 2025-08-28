using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FinanceManager.Domain.Models;

namespace FinanceManager.Application.Dtos.TransactionCategory
{
    public class TransactionCategoryResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CategoryType Type { get; set; }
    }
}

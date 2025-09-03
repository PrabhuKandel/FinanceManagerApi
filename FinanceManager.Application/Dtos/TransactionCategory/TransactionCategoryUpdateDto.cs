using System.ComponentModel.DataAnnotations;
using FinanceManager.Domain.Entities;

namespace FinanceManager.Application.Dtos.TransactionCategory
{
    public class TransactionCategoryUpdateDto
    {
      
        public string Name { get; set; }

        public string? Description { get; set; }

        public CategoryType Type { get; set; }
    }
}

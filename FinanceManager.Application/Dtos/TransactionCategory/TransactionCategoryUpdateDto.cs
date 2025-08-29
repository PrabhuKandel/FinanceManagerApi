using System.ComponentModel.DataAnnotations;
using FinanceManager.Domain.Models;

namespace FinanceManager.Application.Dtos.TransactionCategory
{
    public class TransactionCategoryUpdateDto
    {
      
        [Required]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
        public string? Description { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Invalid CategoryType value.")]
        public CategoryType Type { get; set; }
    }
}

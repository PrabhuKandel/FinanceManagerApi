using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Domain.Models;

namespace FinanceManager.Application.Dtos.TransactionCategory
{
    public class TransactionCategoryUpdateDto
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        [Required]
        public CategoryType Type { get; set; }
    }
}

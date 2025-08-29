using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Domain.Models
{
    public class TransactionCategory
    {
       
        [Key]
        [Required]
        public Guid Id { get; set; } =  Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
        public string? Description { get; set; }

        public CategoryType Type { get; set; }  

      
    }

    public enum CategoryType{

        Income,
        Expense
    }
}

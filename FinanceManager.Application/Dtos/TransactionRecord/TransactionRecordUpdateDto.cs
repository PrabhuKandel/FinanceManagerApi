using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Application.Dtos.TransactionRecord
{
    public class TransactionRecordUpdateDto
    {
        [Required(ErrorMessage = "Transaction category is required.")]
        public Guid TransactionCategoryId { get; set; }


        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Payment method is required.")]
        public Guid PaymentMethodId { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Transaction date is required.")]
        public DateTime TransactionDate { get; set; }
    }
}

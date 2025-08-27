using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Models
{
    public class Transaction
    {
        [Key]
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid TransactionCategoryId{ get; set; }
        public TransactionCategory TransactionCategory { get; set; }

        public double Amount { get; set; }

        public Guid PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
    }
}

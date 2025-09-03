using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FinanceManager.Domain.Models
{
    public class ApplicationUser :IdentityUser
    {
       
        public string FirstName { get; set; }

        public string LastName { get; set; }

       
        public string Address { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiresAtUtc { get; set; }

        // Transactions created by this user
        public ICollection<TransactionRecord>? CreatedTransactionsRecords { get; set; }

        // Transactions updated by this user
        public ICollection<TransactionRecord>? UpdatedTransactionsRecords { get; set; }
    }
}

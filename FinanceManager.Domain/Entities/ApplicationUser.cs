using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace FinanceManager.Domain.Entities
{
    public class ApplicationUser :IdentityUser
    {
       
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

       
        public required string Address { get; set; }

        public  bool IsManuallyLocked { get; set; } = false;

        public string? LockReason { get; set; }

        public ICollection<RefreshToken>? RefreshTokens { get; set; }

        // Transactions created by this user
        public ICollection<TransactionRecord>? CreatedTransactionsRecords { get; set; }

        // Transactions updated by this user
        public ICollection<TransactionRecord>? UpdatedTransactionsRecords { get; set; }
    }
}

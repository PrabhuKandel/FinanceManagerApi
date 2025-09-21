using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Entities
{
        public class RefreshToken
        {
            public Guid Id { get; set; }                
            public required string Token { get; set; }            
            public DateTime ExpiresAt { get; set; }     
            public DateTime CreatedAt { get; set; }     
            public string? DeviceInfo { get; set; }          
            public DateTime? RevokedAt { get; set; }     
            public string? RevocationReason { get; set; }
            public required string ApplicationUserId { get; set; }           
            public ApplicationUser? ApplicationUser { get; set; }

    }
}

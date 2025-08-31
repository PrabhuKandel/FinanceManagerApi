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
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [MaxLength(20)]
        public string Address { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiresAtUtc { get; set; }
    }
}

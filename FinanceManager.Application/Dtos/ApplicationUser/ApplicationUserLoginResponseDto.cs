using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Application.Dtos.ApplicationUser
{
    public class ApplicationUserLoginResponseDto
    {
        public required string UserId { get; set; }
        public  required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }

        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}

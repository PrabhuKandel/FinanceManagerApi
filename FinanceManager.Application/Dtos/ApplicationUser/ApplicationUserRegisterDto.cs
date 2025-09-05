using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Application.Dtos.ApplicationUser
{
    public class ApplicationUserRegisterDto
    {

        
        public required string FirstName { get; set; }

      
        public required string LastName { get; set; }

        

        public required string Address { get; set; }

   
        public required string Email { get; set; }

     
        public required string Password { get; set; }

        
        public string? RoleId { get; set; }
    }
}

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

        
        public string FirstName { get; set; }

      
        public string LastName { get; set; }

        

        public string Address { get; set; }

   
        public string Email { get; set; }

     
        public string Password { get; set; }

        
        public string? RoleId { get; set; }
    }
}

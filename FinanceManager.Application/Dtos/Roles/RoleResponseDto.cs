using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Application.Dtos.Roles
{
    public class RoleResponseDto
    {
        public required string Id { get; set; } 
        public required  string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Application.Features.Roles.Dtos
{
    public class RoleResponseDto
    {
        public required string Id { get; set; } 
        public required  string Name { get; set; }

        public List<string> Permissions { get; set; } = new List<string>();

    }
}

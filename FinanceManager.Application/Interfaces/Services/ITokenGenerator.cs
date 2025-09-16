using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Domain.Entities;

namespace FinanceManager.Application.Interfaces.Services
{
    public interface ITokenGenerator
    {
        Task<string> GenerateAccessToken(ApplicationUser user);
        string GenerateRefreshToken();
    }
}

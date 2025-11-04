
using FinanceManager.Domain.Entities;

namespace FinanceManager.Application.Interfaces.Services
{
    public interface ITokenGenerator
    {
        Task<string> GenerateAccessToken(ApplicationUser user);
        string GenerateRefreshToken();
    }
}

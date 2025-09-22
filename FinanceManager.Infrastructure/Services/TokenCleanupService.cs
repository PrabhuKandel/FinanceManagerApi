
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Interfaces.Services;
using Serilog;


namespace FinanceManager.Infrastructure.Services
{
    public class TokenCleanupService(IApplicationDbContext context) : ITokenCleanupService
    {
        public async Task DeleteRevokedTokensAsync()
        {
            var revokedTokens = context.RefreshTokens.Where(t => t.RevokedAt!=null).Take(2).ToList();

            if (revokedTokens.Any())
            {
                context.RefreshTokens.RemoveRange(revokedTokens);
                await context.SaveChangesAsync();

                Log.Information($"Deleted {revokedTokens.Count} revoked tokens.");
            }
        }
    }
}

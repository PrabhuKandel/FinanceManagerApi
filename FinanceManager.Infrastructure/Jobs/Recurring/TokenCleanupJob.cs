using FinanceManager.Application.Interfaces.Services;

namespace FinanceManager.Infrastructure.Jobs.Recurring
{
    public class TokenCleanupJob(ITokenCleanupService tokenCleanupService)
    {

        public async Task ExecuteAsync()
        {
            await tokenCleanupService.DeleteRevokedTokensAsync();

        }
    }
}

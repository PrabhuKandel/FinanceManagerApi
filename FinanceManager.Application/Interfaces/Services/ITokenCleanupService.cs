namespace FinanceManager.Application.Interfaces.Services
{
    public interface ITokenCleanupService
    {
        Task DeleteRevokedTokensAsync();
    }
}

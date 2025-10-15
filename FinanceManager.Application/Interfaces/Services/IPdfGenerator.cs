

namespace FinanceManager.Application.Interfaces.Services
{
    public interface IPdfGenerator
    {
        Task<byte[]> GeneratePdfAsync(string htmlContent);
    }
}

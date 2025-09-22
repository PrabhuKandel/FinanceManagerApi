

namespace FinanceManager.Application.Interfaces.Services
{
    public interface IEmailJobScheduler
    {
        void EnqueuePasswordResetEmail(string to, string subject, string body);
    }
}

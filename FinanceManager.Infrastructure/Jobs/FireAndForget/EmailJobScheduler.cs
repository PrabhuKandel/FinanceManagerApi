

using FinanceManager.Application.Interfaces.Services;
using Hangfire;
using Serilog;

namespace FinanceManager.Infrastructure.Jobs.FireAndForget
{
    public class EmailJobScheduler:IEmailJobScheduler
    {
        public void EnqueuePasswordResetEmail(string to, string subject, string body)
        {

            BackgroundJob.Enqueue<IEmailService>(x => x.SendEmailAsync(to,subject,body));
            Log.Information("Enqueued email job to {To} with subject {Subject}", to, subject);

        }


    }
}

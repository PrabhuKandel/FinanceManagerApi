
using FinanceManager.Infrastructure.Jobs.Recurring;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Infrastructure.Jobs.Registration
{
    public class HangfireJobSchedular
    {
        public static void RegisterJobs(IServiceProvider serviceProvider)
        {
            var recurringJobManager = serviceProvider.GetRequiredService<IRecurringJobManager>();

            recurringJobManager.AddOrUpdate<TokenCleanupJob>(
                "delete-revoked-tokens",
                job => job.ExecuteAsync(),
                Cron.Daily);

            // Add more recurring jobs here
        }

        
    }
}

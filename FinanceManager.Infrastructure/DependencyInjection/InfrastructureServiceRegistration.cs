using System.Data;
using FinanceManager.Application.Features.TransactionRecords.Queries.ExportToPdf;
using FinanceManager.Application.Interfaces;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Infrastructure.Authorization.Requirements;
using FinanceManager.Infrastructure.Data;
using FinanceManager.Infrastructure.Helpers;
using FinanceManager.Infrastructure.Jobs.FireAndForget;
using FinanceManager.Infrastructure.Jobs.Recurring;
using FinanceManager.Infrastructure.Services;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IDbConnection>(sp =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                return new SqlConnection(connectionString);
            });


            services.AddScoped<IApplicationDbContext>(provider =>
                provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddTransient<IEmailService, MailKitEmailService>();
            services.AddScoped<IEmailJobScheduler, EmailJobScheduler>();
            services.AddScoped<ITokenCleanupService, TokenCleanupService>();
            services.AddScoped<ITransactionAttachmentService, TransactionAttachmentService>();
            services.AddTransient<ITransactionRecordExportService, TransactionRecordExportService>();
            services.AddSingleton<IPdfGenerator, PuppeteerPdfGenerator>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();

            services.AddSingleton<ITemplateRenderer>(sp =>
            {
                var appAssembly = typeof(ExportTransactionRecordsToPdfQuery).Assembly;
                return new HandlebarsTemplateRenderer(appAssembly);

            });
                
            services.AddScoped<TokenCleanupJob>();

            HandlebarsHelpers.RegisterHandlers();
            return services;
        }
    }
}

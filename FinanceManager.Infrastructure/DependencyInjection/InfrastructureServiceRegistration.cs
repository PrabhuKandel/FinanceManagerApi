using System.Data;
using FinanceManager.Application.Interfaces;
using FinanceManager.Infrastructure.Data;
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

            return services;
        }
    }
}

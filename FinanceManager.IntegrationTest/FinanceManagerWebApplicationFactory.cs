using FinanceManager.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MediatR;
using System.Data;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Data.SqlClient;


namespace FinanceManager.IntegrationTest
{
    public class FinanceManagerWebApplicationFactory :WebApplicationFactory<Program> 
    {

     
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(
                services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                             typeof(DbContextOptions<ApplicationDbContext>));


                    if (descriptor != null)
                    {
                        services.Remove(descriptor);

                    }
                    services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(GetConnectionString()));

                    services.RemoveAll<IDbConnection>();
                    services.AddScoped<IDbConnection>(sp =>
                    {
                       return new SqlConnection(GetConnectionString());
            
                    });

                    // Apply migrations
                    var serviceProvider = services.BuildServiceProvider();
                    using var scope = serviceProvider.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    scope.ServiceProvider.GetRequiredService<IMediator>();

                //Optional: clean DB before each test run
                    db.Database.EnsureDeleted();
                    db.Database.Migrate();
                });

            
        }

        private static string GetConnectionString()
        {
            // Build the configuration to access the connection string
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.Test.json")
                .Build();

            return configuration.GetConnectionString("DefaultConnection")!;
        }





    }
}

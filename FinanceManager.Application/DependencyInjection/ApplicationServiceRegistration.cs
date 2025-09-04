using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Application.DependencyInjection
{
    public static  class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register MediatR and scan the assembly for handlers
            //services.AddMediatR(confg => confg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            services.AddMediatR(confg => confg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Register other application services if needed
            return services;
        }
    }

}

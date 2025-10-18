using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Infrastructure.Authorization.Policies;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Infrastructure.Authorization.Extensions
{
    public static class AuthorizationServiceExtensions
    {
        public static IServiceCollection AddAppAuthorization(this IServiceCollection services)
        {
            // Add all authorization policies
            services.AddAuthorizationPolicies();

            // You can also add custom authorization handlers here if needed
            // e.g. services.AddSingleton<IAuthorizationHandler, CustomHandler>();

            return services;
        }
    }
}

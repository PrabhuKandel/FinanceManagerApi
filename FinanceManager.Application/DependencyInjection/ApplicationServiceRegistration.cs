using System.Reflection;
using FinanceManager.Application.Behaviors;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceManager.Application.DependencyInjection
{
    public static  class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddMediatR(confg => confg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddHttpContextAccessor();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IUserContext, UserContext>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
          

            return services;    
        }
    }

}

using System.Reflection;
using FinanceManager.Application.Behaviors;
using FinanceManager.Application.Validators.PaymentMethodValidator;
using FinanceManager.Application.Validators.TransactionCategoryValidator;
using FluentValidation;
using MediatR;
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
            // Register all FluentValidation validators from this assembly (includes DTO and Command validators)
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // Register other application services if needed
            return services;    
        }
    }

}

using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AccountingPayment.Application.DependencyInjetion
{
    public static class ConfigurationApplication
    {
        public static IServiceCollection ConfigureDependencyApplicationInjection(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}

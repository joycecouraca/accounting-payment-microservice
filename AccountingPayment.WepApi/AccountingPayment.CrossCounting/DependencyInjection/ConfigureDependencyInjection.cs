using AccountingPayment.Application.DependencyInjetion;
using AccountingPayment.Domain.Interfaces.ApplicationInsights;
using AccountingPayment.Infrastructure.ApplicationInsights.CustomLog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccountingPayment.CrossCutting.DependencyInjection
{
    public static class ConfigureDependencyInjection
    {
        public static IServiceCollection ConfigureDependencyGeralInjection(this IServiceCollection services, IConfiguration configuration)
        {

            services.ConfigureDependenciesDbContext(configuration);
            services.ConfigureDependenciesRepository();
            services.ConfigureDependencyApplicationInjection();

            services.AddTransient<ICustomLog, CustomLog>();
            return services;
        }
    }
}

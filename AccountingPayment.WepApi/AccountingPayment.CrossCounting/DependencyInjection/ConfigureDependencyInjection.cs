using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AccountingPayment.Application.DependencyInjetion;
namespace AccountingPayment.CrossCutting.DependencyInjection
{
    public static class ConfigureDependencyInjection
    {
        public static IServiceCollection ConfigureDependencyGeralInjection(this IServiceCollection services, IConfiguration configuration)
        {

            services.ConfigureDependenciesDbContext(configuration);
            services.ConfigureDependenciesRepository();
            services.ConfigureDependenciesAutoMapper();

            services.ConfigureDependencyApplicationInjection();
            return services;
        }

    }
}

using AccountingPayment.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccountingPayment.CrossCutting.DependencyInjection
{
    public static class ContextConfiguration
    {
        public static IServiceCollection ConfigureDependenciesDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts
            var res = configuration.GetConnectionString("SQL");

            services.AddDbContext<SqlDbContext>(options =>
            {
                options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStrings_SQL") ?? configuration.GetConnectionString("SQL")!);
            });
            //services.AddScoped(provider => provider.GetRequiredService<SqlDbContext>());
            #endregion

            return services;
        }
    }
}

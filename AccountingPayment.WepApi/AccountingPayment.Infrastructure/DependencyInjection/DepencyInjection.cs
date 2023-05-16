using AccountingPayment.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccountingPayment.Infrastructure.DependencyInjection
{
    public static class DepencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts
            services.AddDbContext<SqlDbContext>(options =>
                options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStrings_SQL") ?? configuration.GetConnectionString("SQL"),
                    builder => builder.MigrationsAssembly(typeof(SqlDbContext).Assembly.FullName)));
            services.AddScoped<SqlDbContext>(provider => provider.GetRequiredService<SqlDbContext>());
            #endregion

            return services;
        }
    }
}

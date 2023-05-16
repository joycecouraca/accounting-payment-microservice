using AccountingPayment.Domain.Interfaces.Repository;
using AccountingPayment.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace AccountingPayment.CrossCutting.DependencyInjection
{
    public static class RepositoryConfiguration
    {
        public static IServiceCollection ConfigureDependenciesRepository(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            return serviceCollection;
        }
    }
}

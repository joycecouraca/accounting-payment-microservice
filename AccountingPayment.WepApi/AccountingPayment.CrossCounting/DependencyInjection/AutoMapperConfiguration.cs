using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AccountingPayment.CrossCutting.DependencyInjection
{
    internal static class AutoMapperConfiguration
    {
        internal static IServiceCollection ConfigureDependenciesAutoMapper(this IServiceCollection serviceCollection)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });

            IMapper mapper = config.CreateMapper();

            serviceCollection.AddSingleton(mapper);

            return serviceCollection;
        }

    }
}

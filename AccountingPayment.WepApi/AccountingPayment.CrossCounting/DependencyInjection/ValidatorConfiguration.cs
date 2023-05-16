using AccountingPayment.Application.UserCases.Employee.Validations;
using AccountingPayment.Domain.Dtos.Employee.Request;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AccountingPayment.CrossCutting.DependencyInjection
{
    internal static class ValidatorConfiguration
    {
        internal static IServiceCollection ConfigureDependencieValidator(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IValidator<EmployeeCreateRequest>, EmployeeCreateValidation>();
            serviceCollection.AddScoped<IValidator<EmployeeUpdateRequest>, EmployeeUpdateValidation>();

            return serviceCollection;
        }
    }
}

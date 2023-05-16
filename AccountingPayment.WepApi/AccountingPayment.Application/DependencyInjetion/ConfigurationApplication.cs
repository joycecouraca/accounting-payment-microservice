using AccountingPayment.Application.UserCases.Employee.Validations;
using AccountingPayment.Domain.Dtos.Employee.Request;
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
            services.AddScoped<IValidator<EmployeeCreateRequest>, EmployeeCreateValidation>();
            services.AddScoped<IValidator<EmployeeUpdateRequest>, EmployeeUpdateValidation>();

            return services;
        }
    }
}

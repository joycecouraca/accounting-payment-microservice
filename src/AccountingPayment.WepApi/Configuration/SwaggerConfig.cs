using Microsoft.OpenApi.Models;

namespace AccountingPayment.WepApi.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "AccountingPayment API",
                    Description = "An ASP.NET Core Web API for Accounting and Payment",
                    Contact = new OpenApiContact
                    {
                        Name = "Contact Suport",
                        Email = "joyce.couraca@hotmail.com"
                    }
                });
            });

            return services;
        }

        public static IApplicationBuilder AddSwaggerBuilder(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}

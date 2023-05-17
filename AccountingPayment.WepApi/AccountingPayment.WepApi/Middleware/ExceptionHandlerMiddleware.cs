using AccountingPayment.Domain.Interfaces.ApplicationInsights;
using System.Net;
using System.Text.Json;

namespace AccountingPayment.WepApi.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICustomLog _logger;
        public ExceptionHandlerMiddleware(RequestDelegate next, ICustomLog logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                error = new
                {
                    message = exception.Message,
                    stackTrace = exception.StackTrace
                }
            };

            await JsonSerializer.SerializeAsync(context.Response.Body, response);
        }
    }
}

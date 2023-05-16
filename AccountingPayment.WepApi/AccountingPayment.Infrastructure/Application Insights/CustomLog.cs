using AccountingPayment.Domain.Interfaces.ApplicationInsights;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace AccountingPayment.Infrastructure.ApplicationInsights.CustomLog
{
    public class CustomLog : ICustomLog
    {
        private TelemetryClient _telemetryClient;
        public CustomLog(IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("CustomLog_ConnectionString") ?? configuration.GetSection("CustomLog:ConnectionString").Value;
            if (String.IsNullOrEmpty(connectionString))
                throw new ArgumentException("CustomLog ConnectionString is null or empty");

            var applicationName = Environment.GetEnvironmentVariable("CustomLog_ApplicationName") ?? configuration.GetSection("CustomLog:ApplicationName").Value;
            if (String.IsNullOrEmpty(connectionString))
                throw new ArgumentException("CustomLog - ApplicationName is null or empty");

            _telemetryClient = new TelemetryClient(new TelemetryConfiguration()
            {
                ConnectionString = connectionString
            });

            _telemetryClient.Context.GlobalProperties.Add("ApplicationName", applicationName);
        }

        public void ErrorLog(Exception exception, Guid? employeeId = null)
        {
            SetEmployeeId(employeeId);

            _telemetryClient.TrackException(exception);
        }

        public void ErrorLog(string message, Guid? employeeId = null)
        {
            SetEmployeeId(employeeId);

            _telemetryClient.TrackTrace(message, Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Error);
        }

        public void InformationLog(object customObject, Guid? employeeId = null)
        {
            SetEmployeeId(employeeId);

            _telemetryClient.TrackTrace(JsonSerializer.Serialize(customObject), Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Information);
        }

        public void InformationLog(string message, Guid? employeeId = null)
        {
            SetEmployeeId(employeeId);

            _telemetryClient.TrackTrace(message, Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Information);
        }

        private void SetEmployeeId(Guid? employeeId)
        {
            if (employeeId.HasValue)
                _telemetryClient.Context.User.Id = employeeId.HasValue.ToString();
        }
    }
}

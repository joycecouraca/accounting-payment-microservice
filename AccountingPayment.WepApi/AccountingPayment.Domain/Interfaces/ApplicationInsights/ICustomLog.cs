namespace AccountingPayment.Domain.Interfaces.ApplicationInsights
{
    public interface ICustomLog
    {
        public void InformationLog(object customObject, Guid? employeeId = null);

        public void InformationLog(string message, Guid? employeeId = null);

        public void ErrorLog(string message, Guid? employeeId = null);

        public void ErrorLog(Exception exception, Guid? employeeId = null);
    }
}

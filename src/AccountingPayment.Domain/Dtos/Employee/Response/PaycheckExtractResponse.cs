namespace AccountingPayment.Domain.Dtos.Employee.Response
{
    public class PaycheckExtractResponse
    {
        public EmployeeResponse Employee { get; set; }
        public string MonthReference { get; set; }
        public decimal NetSalary { get; set; }
        public decimal TotalDiscounts { get; set; }
        public IEnumerable<Release> Releases { get; set; }
    }
    public class Release
    {
        public string Type { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
    }
}

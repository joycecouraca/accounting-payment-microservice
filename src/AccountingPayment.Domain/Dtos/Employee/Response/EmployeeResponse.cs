using AccountingPayment.Domain.Dtos.Sector.Response;

namespace AccountingPayment.Domain.Dtos.Employee.Response
{
    public class EmployeeResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public double GrossSalary { get; set; }
        public DateTime AdmissionDate { get; set; }
        public bool HealthPlanDiscount { get; set; }
        public bool DentalPlanDiscount { get; set; }
        public bool TransportationVoucherDiscount { get; set; }
        public SectorResponse Sector { get; set; }
    }
}

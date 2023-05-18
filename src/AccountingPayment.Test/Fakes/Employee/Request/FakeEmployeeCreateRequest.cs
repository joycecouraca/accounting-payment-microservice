using AccountingPayment.Domain.Dtos.Employee.Request;

namespace AccountingPayment.Test.Fakes.Employee.Request
{
    public class FakeEmployeeCreateRequest : EmployeeCreateRequest
    {
        public FakeEmployeeCreateRequest()
        {
            Name = "John";
            LastName = "Doe";
            Document = "123456789";
            SectorId = Guid.NewGuid();
            GrossSalary = 1000.0;
            AdmissionDate = DateTime.Now;
            HealthPlanDiscount = true;
            DentalPlanDiscount = false;
            TransportationVoucherDiscount = true;
        }
    }
}

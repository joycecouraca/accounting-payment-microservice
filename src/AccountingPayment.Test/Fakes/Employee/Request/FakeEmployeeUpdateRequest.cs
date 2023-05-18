using AccountingPayment.Domain.Dtos.Employee.Request;

namespace AccountingPayment.Test.Fakes.Employee.Request
{
    public class FakeEmployeeUpdateRequest : EmployeeUpdateRequest
    {
        public FakeEmployeeUpdateRequest()
        {
            Id = Guid.NewGuid();
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

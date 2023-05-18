using AccountingPayment.Domain.Entities;

namespace AccountingPayment.Test.Fakes.Employee.Entity
{
    public class FakeEmployeeEntity : EmployeeEntity
    {
        public FakeEmployeeEntity(Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
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
        public EmployeeEntity GetEmployeeEntityPaycheckExtract()
        {
            Id = Guid.Parse("d1cb0bbf-e760-40f6-a856-4610d573bd98");
            Name = "Joyce";
            LastName = "Couraça";
            Document = "44812164842";
            GrossSalary = 1000;
            AdmissionDate = DateTime.Parse("2023-05-15T19:52:31.52");
            HealthPlanDiscount = true;
            DentalPlanDiscount = true;
            TransportationVoucherDiscount = true;
            SectorId = Guid.Parse("6b6ec5d2-8f22-4a8a-9c39-19b66e4b8c01");
            return this;
        }
    }
}

using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Dtos.Employee.Response;
using MediatR;

namespace AccountingPayment.Domain.Dtos.Employee.Request
{
    public class EmployeeCreateRequest : IRequest<ApplicationResult<EmployeeResponse>>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public Guid SectorId { get; set; }
        public double GrossSalary { get; set; }
        public DateTime AdmissionDate { get; set; }
        public bool HealthPlanDiscount { get; set; }
        public bool DentalPlanDiscount { get; set; }
        public bool TransportationVoucherDiscount { get; set; }
    }
}

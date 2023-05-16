using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Dtos.Employee.Response;
using MediatR;

namespace AccountingPayment.Domain.Dtos.Employee.Request
{
    public class EmployeeUpdateRequest : EmployeeCreateRequest, IRequest<ApplicationResult<EmployeeResponse>>
    {
        public Guid Id { get; set; }
    }
}

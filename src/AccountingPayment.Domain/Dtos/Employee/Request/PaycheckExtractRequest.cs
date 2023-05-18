using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Dtos.Employee.Response;
using MediatR;

namespace AccountingPayment.Domain.Dtos.Employee.Request
{
    public class PaycheckExtractRequest : IRequest<ApplicationResult<PaycheckExtractResponse>>
    {
        public Guid EmployeeId { get; set; }
        public string MonthReference { get; set; }
    }
}

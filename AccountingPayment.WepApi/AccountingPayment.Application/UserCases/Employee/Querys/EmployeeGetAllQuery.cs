using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Dtos.Employee.Response;
using AccountingPayment.Domain.Interfaces.Repository;
using Mapster;
using MediatR;

namespace AccountingPayment.Application.UserCases.Employee.Querys
{
    public record EmployeeGetAllQuery() : IRequest<ApplicationResult<IEnumerable<EmployeeResponse>>>;

    public class EmployeeGetAllQueryHandler : IRequestHandler<EmployeeGetAllQuery, ApplicationResult<IEnumerable<EmployeeResponse>>>
    {
        private IEmployeeRepository _repositoryEmployee;

        public EmployeeGetAllQueryHandler(IEmployeeRepository repositoryEmployee)
        {
            _repositoryEmployee = repositoryEmployee;
        }

        public async Task<ApplicationResult<IEnumerable<EmployeeResponse>>> Handle(EmployeeGetAllQuery request, CancellationToken cancellationToken)
        {
            var employeeList = await _repositoryEmployee.SelectAsync();

            if (employeeList is null)
                return new ApplicationResult<IEnumerable<EmployeeResponse>>().ReponseError("NotFound", "Employee not Found");

            return new ApplicationResult<IEnumerable<EmployeeResponse>>().ReponseSuccess(employeeList.Adapt<IEnumerable<EmployeeResponse>>());
        }
    }
}

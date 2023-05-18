using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Dtos.Employee.Response;
using AccountingPayment.Domain.Entities;
using AccountingPayment.Domain.Interfaces.Repository;
using Mapster;
using MediatR;

namespace AccountingPayment.Application.UserCases.Employee.Querys
{
    public record EmployeeGetByIdQuery(Guid employeeId) : IRequest<ApplicationResult<EmployeeResponse>>;

    public class EmployeeGetByIdQueryHandler : IRequestHandler<EmployeeGetByIdQuery, ApplicationResult<EmployeeResponse>>
    {
        private IEmployeeRepository<EmployeeEntity> _repositoryEmployee;

        public EmployeeGetByIdQueryHandler(IEmployeeRepository<EmployeeEntity> repositoryEmployee)
        {
            _repositoryEmployee = repositoryEmployee;
        }

        public async Task<ApplicationResult<EmployeeResponse>> Handle(EmployeeGetByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _repositoryEmployee.SelectAsync(request.employeeId);

            if (employee is null)
                return new ApplicationResult<EmployeeResponse>().ReponseError("NotFound", "Employee not Found");

            return new ApplicationResult<EmployeeResponse>().ReponseSuccess(employee.Adapt<EmployeeResponse>());
        }
    }
}

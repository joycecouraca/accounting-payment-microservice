using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Entities;
using AccountingPayment.Domain.Interfaces.Repository;
using MediatR;

namespace AccountingPayment.Application.UserCases.Employee.Commands
{
    public record EmployeeDeleteCommand(Guid employeeId) : IRequest<ApplicationResult<bool>>;

    public class EmployeeDeleteCommandHandler : IRequestHandler<EmployeeDeleteCommand, ApplicationResult<bool>>
    {
        private IEmployeeRepository<EmployeeEntity> _repositoryEmployee;
        public EmployeeDeleteCommandHandler(IEmployeeRepository<EmployeeEntity> repositoryEmployee)
        {
            _repositoryEmployee = repositoryEmployee;
        }

        public async Task<ApplicationResult<bool>> Handle(EmployeeDeleteCommand request, CancellationToken cancellationToken)
        {
            var result = await _repositoryEmployee.DeleteAsync(request.employeeId);

            return new ApplicationResult<bool>().ReponseSuccess(result);
        }
    }
}

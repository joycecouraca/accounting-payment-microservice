using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Dtos.Employee.Request;
using AccountingPayment.Domain.Dtos.Employee.Response;
using AccountingPayment.Domain.Entities;
using AccountingPayment.Domain.Interfaces.Repository;
using FluentValidation;
using Mapster;
using MediatR;

namespace AccountingPayment.Application.UserCases.Employee.Commands
{
    public class EmployeeUpdateCommandHandler : IRequestHandler<EmployeeUpdateRequest, ApplicationResult<EmployeeResponse>>
    {
        private IEmployeeRepository<EmployeeEntity> _repositoryEmployee;
        private readonly IValidator<EmployeeUpdateRequest> _validator;
        public EmployeeUpdateCommandHandler(IEmployeeRepository<EmployeeEntity> repositoryEmployee, IValidator<EmployeeCreateRequest> validator)
        {
            _repositoryEmployee = repositoryEmployee;
            _validator = validator;
        }

        public async Task<ApplicationResult<EmployeeResponse>> Handle(EmployeeUpdateRequest request, CancellationToken cancellationToken)
        {
            var resultValidation = await _validator.ValidateAsync(request);

            if (!resultValidation.IsValid)
            {
                var res = resultValidation.Errors.Select(c => new ApplicationError(c.ErrorCode, c.ErrorMessage)).ToList();
                return new ApplicationResult<EmployeeResponse>().ReponseErrorFluentValidator(res);
            }

            var employeeEntity = request.Adapt<EmployeeEntity>();

            var result = await _repositoryEmployee.UpdateAsync(employeeEntity);

            result = await _repositoryEmployee.SelectAsync(result.Id);

            return new ApplicationResult<EmployeeResponse>().ReponseSuccess(result.Adapt<EmployeeResponse>());
        }
    }
}

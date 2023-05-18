using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Dtos.Employee.Request;
using AccountingPayment.Domain.Dtos.Employee.Response;
using AccountingPayment.Domain.Entities;
using AccountingPayment.Domain.Interfaces.Repository;
using AccountingPayment.Domain.Util.Extension;
using FluentValidation;
using Mapster;
using MediatR;
using System.Text.RegularExpressions;

namespace AccountingPayment.Application.UserCases.Employee.Commands
{
    public class EmployeeCreateCommandHandler : IRequestHandler<EmployeeCreateRequest, ApplicationResult<EmployeeResponse>>
    {
        private IEmployeeRepository<EmployeeEntity> _repositoryEmployee;
        private readonly IValidator<EmployeeCreateRequest> _validator;
        public EmployeeCreateCommandHandler(IEmployeeRepository<EmployeeEntity> repositoryEmployee, IValidator<EmployeeCreateRequest> validator)
        {
            _repositoryEmployee = repositoryEmployee;
            _validator = validator;

        }

        public async Task<ApplicationResult<EmployeeResponse>> Handle(EmployeeCreateRequest request, CancellationToken cancellationToken)
        {
            var resultValidation = await _validator.ValidateAsync(request);

            if (!resultValidation.IsValid)
            {
                var res = resultValidation.Errors.Select(c => new ApplicationError(c.ErrorCode, c.ErrorMessage)).ToList();
                return new ApplicationResult<EmployeeResponse>().ReponseErrorFluentValidator(res);
            }

            var employeeEntity = request.Adapt<EmployeeEntity>();

            employeeEntity.Document = employeeEntity.Document.RemoveMaskCpf();

            var result = await _repositoryEmployee.InsertAsync(employeeEntity);

            result = await _repositoryEmployee.SelectAsync(result.Id);

            return new ApplicationResult<EmployeeResponse>().ReponseSuccess(result.Adapt<EmployeeResponse>());
        }
    }
}

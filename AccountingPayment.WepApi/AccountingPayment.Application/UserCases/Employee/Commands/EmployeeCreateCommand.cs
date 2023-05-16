using AccountingPayment.Application.UserCases.Employee.Mappers;
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
    public class EmployeeCreateCommandHandler : IRequestHandler<EmployeeCreateRequest, ApplicationResult<EmployeeResponse>>
    {
        private IRepository<EmployeeEntity> _repositoryEmployee;
        private IRepository<SectorEntity> _repositorySector;
        private readonly IValidator<EmployeeCreateRequest> _validator;
        public EmployeeCreateCommandHandler(IRepository<EmployeeEntity> repository, IValidator<EmployeeCreateRequest> validator, IRepository<SectorEntity> repositorySector)
        {
            _repositoryEmployee = repository;
            _validator = validator;
            _repositorySector = repositorySector;
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

            var result = await _repositoryEmployee.InsertAsync(employeeEntity);

            var sector = await _repositorySector.SelectAsync(result.SectorId);

            return new ApplicationResult<EmployeeResponse>().ReponseSuccess(result.EmployeeToDto(sector));
        }
    }
}

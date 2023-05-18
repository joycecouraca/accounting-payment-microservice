using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Dtos.Employee.Request;
using AccountingPayment.Domain.Dtos.Employee.Response;
using AccountingPayment.Domain.Entities;
using AccountingPayment.Domain.Interfaces.Repository;
using AccountingPayment.Domain.Util.Calculator;
using FluentValidation;
using MediatR;

namespace AccountingPayment.Application.UserCases.PaycheckExtract.Command
{
    public class PaycheckExtractCommandHandler : IRequestHandler<PaycheckExtractRequest, ApplicationResult<PaycheckExtractResponse>>
    {
        private IEmployeeRepository<EmployeeEntity> _repositoryEmployee;
        private readonly IValidator<PaycheckExtractRequest> _validator;

        public PaycheckExtractCommandHandler(IEmployeeRepository<EmployeeEntity> repositoryEmployee, IValidator<PaycheckExtractRequest> validator)
        {
            _repositoryEmployee = repositoryEmployee;
            _validator = validator;
        }

        public async Task<ApplicationResult<PaycheckExtractResponse>> Handle(PaycheckExtractRequest request, CancellationToken cancellationToken)
        {
            var resultValidation = await _validator.ValidateAsync(request);

            if (!resultValidation.IsValid)
            {
                var res = resultValidation.Errors.Select(c => new ApplicationError(c.ErrorCode, c.ErrorMessage)).ToList();
                return new ApplicationResult<PaycheckExtractResponse>().ReponseErrorFluentValidator(res);
            }


            var employeeEntity = await _repositoryEmployee.SelectAsync(request.EmployeeId);

            if (employeeEntity == null)
                return new ApplicationResult<PaycheckExtractResponse>().ReponseError("Employee not Found", "NotFound");

            return new ApplicationResult<PaycheckExtractResponse>().ReponseSuccess(PaycheckExtractorCalculate.GetPaycheckExtract(employeeEntity, request));
        }
    }
}

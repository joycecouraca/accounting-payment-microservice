using AccountingPayment.Application.UserCases.PaycheckExtract.Command;
using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Dtos.Employee.Request;
using AccountingPayment.Domain.Dtos.Employee.Response;
using AccountingPayment.Domain.Entities;
using AccountingPayment.Domain.Interfaces.Repository;
using AccountingPayment.Domain.Util.Calculator;
using AccountingPayment.Test.Fakes.Employee.Entity;
using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace AccountingPayment.Test.UseCase.PaycheckExtract.Commands
{
    public class PaycheckExtractCommandHandlerTests
    {
        private readonly IValidator<PaycheckExtractRequest> _validator;
        private readonly IEmployeeRepository<EmployeeEntity> _repositoryEmployee;
        private readonly PaycheckExtractCommandHandler _handler;

        public PaycheckExtractCommandHandlerTests()
        {
            _validator = A.Fake<IValidator<PaycheckExtractRequest>>();
            _repositoryEmployee = A.Fake<IEmployeeRepository<EmployeeEntity>>();
            _handler = new PaycheckExtractCommandHandler(_repositoryEmployee, _validator);
        }

        [Fact]
        public async Task Handle_ExistingEmployee_ReturnsSuccessResult()
        {
            // Arrange
            var employeeId = Guid.NewGuid(); // Defina um ID válido de funcionário
            var request = new PaycheckExtractRequest { EmployeeId = employeeId, MonthReference = "05-2023" };

            var employee = new FakeEmployeeEntity().GetEmployeeEntityPaycheckExtract();

            A.CallTo(() => _validator.ValidateAsync(request, CancellationToken.None)).Returns(new ValidationResult());

            A.CallTo(() => _repositoryEmployee.SelectAsync(employeeId)).Returns(Task.FromResult(employee));

            var expectedResult = GetExpectedResult(employee, request);

            var handler = new PaycheckExtractCommandHandler(_repositoryEmployee, _validator);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task Handle_NonExistingEmployee_ReturnsErrorResult()
        {
            // Arrange
            var employeeId = Guid.NewGuid(); // Defina um ID inválido de funcionário
            var request = new PaycheckExtractRequest { EmployeeId = employeeId, MonthReference = "05-2023" };

            var repository = A.Fake<IEmployeeRepository<EmployeeEntity>>();
            EmployeeEntity employee = null; // Defina que o funcionário não existe (retornado como nulo pelo repositório)

            var validator = A.Fake<IValidator<PaycheckExtractRequest>>();

            A.CallTo(() => validator.ValidateAsync(request, CancellationToken.None)).Returns(new ValidationResult());

            A.CallTo(() => repository.SelectAsync(employeeId)).Returns(employee);

            var expectedResult = new ApplicationResult<PaycheckExtractResponse>();
            expectedResult.ReponseError("Employee not Found", "NotFound");

            var handler = new PaycheckExtractCommandHandler(repository, validator);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ReturnsErrorResult()
        {
            // Arrange
            var request = new PaycheckExtractRequest();
            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("EmployeeId", "EmployeeId is required"),
                new ValidationFailure("MonthReference", "MonthReference is required")
            });

            var expectedResult = new ApplicationResult<PaycheckExtractResponse>(); // Criar um resultado esperado
            expectedResult.ReponseErrorFluentValidator(validationResult.Errors.Select(c => new ApplicationError(c.ErrorCode, c.ErrorMessage)).ToList());

            A.CallTo(() => _validator.ValidateAsync(request, CancellationToken.None)).Returns(validationResult);

            // Act
            var result = await _handler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            Assert.False(result.Success);
            result.Should().BeEquivalentTo(expectedResult);
        }

        private ApplicationResult<PaycheckExtractResponse> GetExpectedResult(EmployeeEntity employee, PaycheckExtractRequest request)
        {
            var expectedResult = new ApplicationResult<PaycheckExtractResponse>();
            expectedResult.ReponseSuccess(PaycheckExtractorCalculate.GetPaycheckExtract(employee, request));

            return expectedResult;
        }

    }
}

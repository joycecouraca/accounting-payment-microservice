using AccountingPayment.Application.UserCases.Employee.Commands;
using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Dtos.Employee.Request;
using AccountingPayment.Domain.Dtos.Employee.Response;
using AccountingPayment.Domain.Entities;
using AccountingPayment.Domain.Interfaces.Repository;
using AccountingPayment.Test.Fakes.Employee.Entity;
using AccountingPayment.Test.Fakes.Employee.Request;
using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Mapster;
using Xunit;

namespace AccountingPayment.Test.UseCase.Employee.Commands
{
    public class EmployeeUpdateCommandHandlerTests
    {
        private readonly IEmployeeRepository<EmployeeEntity> _repository;
        private readonly IValidator<EmployeeUpdateRequest> _validator;
        private readonly EmployeeUpdateCommandHandler _handler;

        public EmployeeUpdateCommandHandlerTests()
        {
            _repository = A.Fake<IEmployeeRepository<EmployeeEntity>>();
            _validator = A.Fake<IValidator<EmployeeUpdateRequest>>();
            _handler = new EmployeeUpdateCommandHandler(_repository, _validator);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            var request = new EmployeeUpdateRequest();
            var validationResult = new ValidationResult();
            var employeeEntity = new EmployeeEntity();
            var updatedEntity = new EmployeeEntity();
            var expectedResult = new ApplicationResult<EmployeeResponse>().ReponseSuccess(updatedEntity.Adapt<EmployeeResponse>());

            A.CallTo(() => _validator.ValidateAsync(request, A<CancellationToken>._)).Returns(validationResult);
            A.CallTo(() => _repository.UpdateAsync(employeeEntity)).Returns(updatedEntity);
            A.CallTo(() => _repository.SelectAsync(updatedEntity.Id)).Returns(updatedEntity);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
            A.CallTo(() => _validator.ValidateAsync(request, A<CancellationToken>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _repository.SelectAsync(updatedEntity.Id)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Handle_InvalidRequest_ReturnsErrorResult()
        {
            // Arrange
            var request = new FakeEmployeeUpdateRequest();

            var validationResult = new ValidationResult(new List<ValidationFailure> { new ValidationFailure("propertyName", "errorMessage") });

            var expectedResult = new ApplicationResult<EmployeeResponse>().ReponseErrorFluentValidator(validationResult.Errors.Select(c => new ApplicationError(c.ErrorCode, c.ErrorMessage)).ToList());

            A.CallTo(() => _validator.ValidateAsync(request, A<CancellationToken>._)).Returns(validationResult);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
            A.CallTo(() => _validator.ValidateAsync(request, A<CancellationToken>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _repository.UpdateAsync(A<EmployeeEntity>._)).MustNotHaveHappened();
            A.CallTo(() => _repository.SelectAsync(A<Guid>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Handle_UpdateFailed_ReturnsErrorResult()
        {
            // Arrange
            var request = new FakeEmployeeUpdateRequest();
            var validationResult = new ValidationResult();
            var employeeEntity = new FakeEmployeeEntity();
            var expectedResult = new ApplicationResult<EmployeeResponse>().ReponseError("Employee not Found", "NotFound");

            A.CallTo(() => _validator.ValidateAsync(request, A<CancellationToken>._)).Returns(validationResult);
            A.CallTo(() => _repository.UpdateAsync(A<EmployeeEntity>._)).Returns((EmployeeEntity)null);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
            A.CallTo(() => _validator.ValidateAsync(request, A<CancellationToken>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _repository.SelectAsync(A<Guid>._)).MustNotHaveHappened();
        }
    }
}

using AccountingPayment.Application.UserCases.Employee.Querys;
using AccountingPayment.Domain.Dtos.Employee.Response;
using AccountingPayment.Domain.Entities;
using AccountingPayment.Domain.Interfaces.Repository;
using AccountingPayment.Test.Fakes.Employee.Result;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace AccountingPayment.Test.UseCase.Employee.Querys
{
    public class EmployeeGetByIdQueryHandlerTests
    {
        private readonly IEmployeeRepository<EmployeeEntity> _repository;
        private readonly EmployeeGetByIdQueryHandler _handler;

        public EmployeeGetByIdQueryHandlerTests()
        {
            _repository = A.Fake<IEmployeeRepository<EmployeeEntity>>();
            _handler = new EmployeeGetByIdQueryHandler(_repository);
        }

        [Fact]
        public async Task Handle_ReturnsSuccessResult_WhenEmployeeExists()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var employee = new EmployeeEntity();
            var expectedResult = FakeApplicationEmployeeResult<EmployeeResponse>.GetResponseSuccess(employee);

            A.CallTo(() => _repository.SelectAsync(employeeId)).Returns(employee);

            // Act
            var result = await _handler.Handle(new EmployeeGetByIdQuery(employeeId), CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
            A.CallTo(() => _repository.SelectAsync(employeeId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Handle_ReturnsErrorResult_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var expectedResult = FakeApplicationEmployeeResult<EmployeeResponse>.GetResponseErrorNotFound();

            A.CallTo(() => _repository.SelectAsync(employeeId)).Returns((EmployeeEntity)null);

            // Act
            var result = await _handler.Handle(new EmployeeGetByIdQuery(employeeId), CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
            A.CallTo(() => _repository.SelectAsync(employeeId)).MustHaveHappenedOnceExactly();
        }
    }
}

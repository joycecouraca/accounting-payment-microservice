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
    public class EmployeeGetAllQueryHandlerTests
    {
        private readonly IEmployeeRepository<EmployeeEntity> _repository;
        private readonly EmployeeGetAllQueryHandler _handler;

        public EmployeeGetAllQueryHandlerTests()
        {
            _repository = A.Fake<IEmployeeRepository<EmployeeEntity>>();
            _handler = new EmployeeGetAllQueryHandler(_repository);
        }

        [Fact]
        public async Task Handle_ReturnsSuccessResult()
        {
            // Arrange
            var employeeList = new List<EmployeeEntity>();
            var expectedResult = FakeApplicationEmployeeResult<IEnumerable<EmployeeResponse>>.GetResponseSuccessList(employeeList);

            A.CallTo(() => _repository.SelectAsync()).Returns(employeeList);

            // Act
            var result = await _handler.Handle(new EmployeeGetAllQuery(), CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
            A.CallTo(() => _repository.SelectAsync()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Handle_ReturnsErrorResult_WhenEmployeeListIsNull()
        {
            // Arrange
            var expectedResult = FakeApplicationEmployeeResult<IEnumerable<EmployeeResponse>>.GetResponseErrorNotFound();

            A.CallTo(() => _repository.SelectAsync()).Returns((List<EmployeeEntity>)null);

            // Act
            var result = await _handler.Handle(new EmployeeGetAllQuery(), CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
            A.CallTo(() => _repository.SelectAsync()).MustHaveHappenedOnceExactly();
        }
    }
}

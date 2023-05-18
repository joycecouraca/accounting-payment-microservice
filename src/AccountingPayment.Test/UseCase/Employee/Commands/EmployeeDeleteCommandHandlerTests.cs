using AccountingPayment.Application.UserCases.Employee.Commands;
using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Entities;
using AccountingPayment.Domain.Interfaces.Repository;
using FakeItEasy;
using Xunit;

namespace AccountingPayment.Test.UseCase.Employee.Commands
{
    public class EmployeeDeleteCommandHandlerTests
    {
        private readonly IEmployeeRepository<EmployeeEntity> _repository;
        private readonly EmployeeDeleteCommandHandler _handler;

        public EmployeeDeleteCommandHandlerTests()
        {
            _repository = A.Fake<IEmployeeRepository<EmployeeEntity>>();
            _handler = new EmployeeDeleteCommandHandler(_repository);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var expectedResult = new ApplicationResult<bool>().ReponseSuccess(true);

            A.CallTo(() => _repository.DeleteAsync(employeeId)).Returns(true);

            var command = new EmployeeDeleteCommand(employeeId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            A.CallTo(() => _repository.DeleteAsync(employeeId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Handle_InvalidRequest_ReturnsErrorResult()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var expectedResult = new ApplicationResult<bool?>().ReponseError("Employee not Found", "NotFound");

            A.CallTo(() => _repository.DeleteAsync(employeeId)).Returns((bool?)null);

            var command = new EmployeeDeleteCommand(employeeId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            A.CallTo(() => _repository.DeleteAsync(employeeId)).MustHaveHappenedOnceExactly();
        }
    }
}

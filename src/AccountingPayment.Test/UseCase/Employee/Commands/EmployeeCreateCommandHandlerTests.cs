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
using Moq;
using Xunit;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace AccountingPayment.Test.UseCase.Employee.Commands
{
    public class EmployeeCreateCommandHandlerTests
    {
        private readonly IEmployeeRepository<EmployeeEntity> _repository;
        private readonly IValidator<EmployeeCreateRequest> _validator;
        private readonly EmployeeCreateCommandHandler _handler;

        public EmployeeCreateCommandHandlerTests()
        {
            _repository = A.Fake<IEmployeeRepository<EmployeeEntity>>();
            _validator = A.Fake<IValidator<EmployeeCreateRequest>>();
            _handler = new EmployeeCreateCommandHandler(_repository, _validator);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            var request = new FakeEmployeeCreateRequest();
            var insertedEntity = new FakeEmployeeEntity(Guid.NewGuid());

            A.CallTo(() => _validator.ValidateAsync(request, CancellationToken.None)).Returns(new ValidationResult());

            A.CallTo(() => _repository.InsertAsync(A<EmployeeEntity>.Ignored)).Returns(insertedEntity);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
        }


        [Fact]
        public async Task Handle_InvalidRequest_ReturnsErrorResult()
        {
            // Arrange
            var request = new EmployeeCreateRequest(); // Criar uma instância do objeto de requisição inválido
            var validationResult = new ValidationResult(new List<ValidationFailure> { new ValidationFailure("propertyName", "errorMessage") }); // Criar um resultado de validação com erros

            var expectedResult = new ApplicationResult<EmployeeResponse>().ReponseErrorFluentValidator(validationResult.Errors.Select(c => new ApplicationError(c.ErrorCode, c.ErrorMessage)).ToList());

            A.CallTo(() => _validator.ValidateAsync(request, CancellationToken.None)).Returns(validationResult);

            // Act
            var result = await _handler.Handle(request, It.IsAny<CancellationToken>());

            // Assert
            Assert.False(result.Success);
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}

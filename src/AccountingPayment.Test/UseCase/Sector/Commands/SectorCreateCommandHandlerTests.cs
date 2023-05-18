using AccountingPayment.Application.UserCases.Sector.Commands;
using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Dtos.Sector.Request;
using AccountingPayment.Domain.Dtos.Sector.Response;
using AccountingPayment.Domain.Entities;
using AccountingPayment.Domain.Interfaces.Repository;
using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Mapster;
using Xunit;

namespace AccountingPayment.Test.UseCase.Sector.Commands
{
    public class SectorCreateCommandHandlerTests
    {
        private readonly IValidator<SectorCreateRequest> _validator;
        private readonly ISectorRepository<SectorEntity> _repository;
        private readonly SectorCreateCommandHandler _handler;

        public SectorCreateCommandHandlerTests()
        {
            _validator = A.Fake<IValidator<SectorCreateRequest>>();
            _repository = A.Fake<ISectorRepository<SectorEntity>>();
            _handler = new SectorCreateCommandHandler(_validator, _repository);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            var request = new SectorCreateRequest();
            var validationResult = new ValidationResult();
            var insertedEntity = new SectorEntity();
            var expectedResult = new ApplicationResult<SectorResponse>();

            A.CallTo(() => _validator.ValidateAsync(request, A<CancellationToken>._))
                .Returns(validationResult);

            A.CallTo(() => _repository.InsertAsync(A<SectorEntity>._))
                .Returns(insertedEntity);

            expectedResult.ReponseSuccess(insertedEntity.Adapt<SectorResponse>());

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ReturnsErrorResult()
        {
            // Arrange
            var request = new SectorCreateRequest();
            var validationResult = new ValidationResult(new List<ValidationFailure> { new ValidationFailure("propertyName", "errorMessage") });

            A.CallTo(() => _validator.ValidateAsync(request, A<CancellationToken>._))
                .Returns(validationResult);

            var expectedResult = new ApplicationResult<SectorResponse>();

            expectedResult.ReponseErrorFluentValidator(validationResult.Errors.Select(c => new ApplicationError(c.ErrorCode, c.ErrorMessage)).ToList());

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}

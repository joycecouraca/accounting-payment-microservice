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
    public class SectorUpdateCommandHandlerTests
    {
        private readonly ISectorRepository<SectorEntity> _repository;
        private readonly IValidator<SectorUpdateRequest> _validator;
        private readonly SectorUpdateCommandHandler _handler;

        public SectorUpdateCommandHandlerTests()
        {
            _repository = A.Fake<ISectorRepository<SectorEntity>>();
            _validator = A.Fake<IValidator<SectorUpdateRequest>>();
            _handler = new SectorUpdateCommandHandler(_validator, _repository);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            var request = new SectorUpdateRequest();
            var validationResult = new ValidationResult();

            var updatedSector = new SectorEntity();
            A.CallTo(() => _repository.UpdateAsync(A<SectorEntity>.Ignored))
                .Returns(updatedSector);

            var expectedResult = new ApplicationResult<SectorResponse>();
            expectedResult.ReponseSuccess(updatedSector.Adapt<SectorResponse>());

            A.CallTo(() => _validator.ValidateAsync(request, CancellationToken.None))
                .Returns(validationResult);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ReturnsErrorResult()
        {
            // Arrange
            var request = new SectorUpdateRequest();
            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("propertyName", "errorMessage"));

            var expectedResult = new ApplicationResult<SectorResponse>();

            expectedResult.ReponseErrorFluentValidator(validationResult.Errors.Select(c => new ApplicationError(c.ErrorCode, c.ErrorMessage)).ToList());

            A.CallTo(() => _validator.ValidateAsync(request, CancellationToken.None))
                .Returns(validationResult);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}

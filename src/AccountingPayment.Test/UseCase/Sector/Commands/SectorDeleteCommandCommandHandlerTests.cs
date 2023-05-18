using AccountingPayment.Application.UserCases.Sector.Commands;
using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Entities;
using AccountingPayment.Domain.Interfaces.Repository;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace AccountingPayment.Test.UseCase.Sector.Commands
{
    public class SectorDeleteCommandCommandHandlerTests
    {
        private readonly ISectorRepository<SectorEntity> _repository;
        private readonly SectorDeleteCommandCommandHandler _handler;

        public SectorDeleteCommandCommandHandlerTests()
        {
            _repository = A.Fake<ISectorRepository<SectorEntity>>();
            _handler = new SectorDeleteCommandCommandHandler(_repository);
        }

        [Fact]
        public async Task Handle_ExistingSector_ReturnsSuccessResult()
        {
            // Arrange
            var sectorId = Guid.NewGuid();
            var deleted = true;

            A.CallTo(() => _repository.DeleteAsync(sectorId)).Returns(deleted);

            var expectedResult = new ApplicationResult<bool>();
            expectedResult.ReponseSuccess(deleted);

            var request = new SectorDeleteCommand(sectorId);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task Handle_NonExistingSector_ReturnsErrorResult()
        {
            // Arrange
            var sectorId = Guid.NewGuid();

            A.CallTo(() => _repository.DeleteAsync(sectorId)).Returns((bool?)null);

            var expectedResult = new ApplicationResult<bool>();
            expectedResult.ReponseError("Sector not Found", "NotFound");

            var request = new SectorDeleteCommand(sectorId);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}

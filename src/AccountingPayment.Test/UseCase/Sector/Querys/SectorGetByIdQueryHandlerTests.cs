using AccountingPayment.Application.UserCases.Sector.Querys;
using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Dtos.Sector.Response;
using AccountingPayment.Domain.Entities;
using AccountingPayment.Domain.Interfaces.Repository;
using FakeItEasy;
using FluentAssertions;
using Mapster;
using Xunit;

namespace AccountingPayment.Test.UseCase.Sector.Querys
{
    public class SectorGetByIdQueryHandlerTests
    {
        private readonly ISectorRepository<SectorEntity> _repository;
        private readonly SectorGetByIdQueryHandler _handler;

        public SectorGetByIdQueryHandlerTests()
        {
            _repository = A.Fake<ISectorRepository<SectorEntity>>();
            _handler = new SectorGetByIdQueryHandler(_repository);
        }

        [Fact]
        public async Task Handle_ExistingSector_ReturnsSuccessResult()
        {
            // Arrange
            var sectorId = Guid.NewGuid();
            var request = new SectorGetByIdQuery(sectorId);

            var sector = new SectorEntity();
            A.CallTo(() => _repository.SelectAsync(sectorId)).Returns(Task.FromResult(sector));

            var expectedResult = new ApplicationResult<SectorResponse>();
            expectedResult.ReponseSuccess(sector.Adapt<SectorResponse>());

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
            var request = new SectorGetByIdQuery(sectorId);

            A.CallTo(() => _repository.SelectAsync(sectorId)).Returns((SectorEntity)null);

            var expectedResult = new ApplicationResult<SectorResponse>();
            expectedResult.ReponseError("NotFound", "Sector not Found");

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}

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

    public class SectorGetAllQueryHandlerTests
    {
        private readonly ISectorRepository<SectorEntity> _repository;
        private readonly SectorGetAllQueryHandler _handler;

        public SectorGetAllQueryHandlerTests()
        {
            _repository = A.Fake<ISectorRepository<SectorEntity>>();
            _handler = new SectorGetAllQueryHandler(_repository);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            var request = new SectorGetAllQuery();

            var sectorList = Enumerable.Empty<SectorEntity>();
            A.CallTo(() => _repository.SelectAsync())
                .Returns(Task.FromResult(sectorList));

            var expectedResult = new ApplicationResult<IEnumerable<SectorResponse>>();
            expectedResult.ReponseSuccess(sectorList.Adapt<IEnumerable<SectorResponse>>());

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task Handle_EmptyList_ReturnsErrorResult()
        {
            // Arrange
            var request = new SectorGetAllQuery();

            List<SectorEntity> sectorList = null;
            A.CallTo(() => _repository.SelectAsync()).Returns(sectorList);

            var expectedResult = new ApplicationResult<IEnumerable<SectorResponse>>();
            expectedResult.ReponseError("NotFound", "Sector not Found");

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}

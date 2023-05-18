using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Dtos.Sector.Response;
using AccountingPayment.Domain.Entities;
using AccountingPayment.Domain.Interfaces.Repository;
using Mapster;
using MediatR;

namespace AccountingPayment.Application.UserCases.Sector.Querys
{
    public record SectorGetAllQuery() : IRequest<ApplicationResult<IEnumerable<SectorResponse>>>;

    public class SectorGetAllQueryHandler : IRequestHandler<SectorGetAllQuery, ApplicationResult<IEnumerable<SectorResponse>>>
    {
        private ISectorRepository<SectorEntity> _repositorySector;

        public SectorGetAllQueryHandler(ISectorRepository<SectorEntity> repositorySector)
        {
            _repositorySector = repositorySector;
        }

        public async Task<ApplicationResult<IEnumerable<SectorResponse>>> Handle(SectorGetAllQuery request, CancellationToken cancellationToken)
        {
            var SectorList = await _repositorySector.SelectAsync();

            if (SectorList is null)
                return new ApplicationResult<IEnumerable<SectorResponse>>().ReponseError("NotFound", "Sector not Found");

            return new ApplicationResult<IEnumerable<SectorResponse>>().ReponseSuccess(SectorList.Adapt<IEnumerable<SectorResponse>>());
        }
    }
}

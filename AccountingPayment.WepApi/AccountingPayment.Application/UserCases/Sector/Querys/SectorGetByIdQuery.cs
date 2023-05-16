using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Dtos.Sector.Response;
using AccountingPayment.Domain.Entities;
using AccountingPayment.Domain.Interfaces.Repository;
using Mapster;
using MediatR;

namespace AccountingPayment.Application.UserCases.Sector.Querys
{
    public record SectorGetByIdQuery(Guid SectorId) : IRequest<ApplicationResult<SectorResponse>>;

    public class SectorGetByIdQueryHandler : IRequestHandler<SectorGetByIdQuery, ApplicationResult<SectorResponse>>
    {
        private ISectorRepository<SectorEntity> _repositorySector;

        public SectorGetByIdQueryHandler(ISectorRepository<SectorEntity> repositorySector)
        {
            _repositorySector = repositorySector;
        }

        public async Task<ApplicationResult<SectorResponse>> Handle(SectorGetByIdQuery request, CancellationToken cancellationToken)
        {
            var Sector = await _repositorySector.SelectAsync(request.SectorId);

            if (Sector is null)
                return new ApplicationResult<SectorResponse>().ReponseError("NotFound", "Sector not Found");

            return new ApplicationResult<SectorResponse>().ReponseSuccess(Sector.Adapt<SectorResponse>());
        }
    }
}

using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Entities;
using AccountingPayment.Domain.Interfaces.Repository;
using MediatR;

namespace AccountingPayment.Application.UserCases.Sector.Commands
{
    public record SectorDeleteCommand(Guid employeeId) : IRequest<ApplicationResult<bool>>;

    public class SectorDeleteCommandCommandHandler : IRequestHandler<SectorDeleteCommand, ApplicationResult<bool>>
    {
        private ISectorRepository<SectorEntity> _repositorySector;
        public SectorDeleteCommandCommandHandler(ISectorRepository<SectorEntity> repositorySector)
        {
            _repositorySector = repositorySector;
        }

        public async Task<ApplicationResult<bool>> Handle(SectorDeleteCommand request, CancellationToken cancellationToken)
        {
            var result = await _repositorySector.DeleteAsync(request.employeeId);

            return new ApplicationResult<bool>().ReponseSuccess(result);
        }
    }
}

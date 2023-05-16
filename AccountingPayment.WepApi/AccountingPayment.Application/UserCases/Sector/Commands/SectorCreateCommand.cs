using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Dtos.Sector.Request;
using AccountingPayment.Domain.Dtos.Sector.Response;
using AccountingPayment.Domain.Entities;
using AccountingPayment.Domain.Interfaces.Repository;
using FluentValidation;
using Mapster;
using MediatR;

namespace AccountingPayment.Application.UserCases.Sector.Commands
{
    public class SectorCreateCommandHandler : IRequestHandler<SectorCreateRequest, ApplicationResult<SectorResponse>>
    {
        private ISectorRepository<SectorEntity> _repositorySector;
        private readonly IValidator<SectorCreateRequest> _validator;
        public SectorCreateCommandHandler(IValidator<SectorCreateRequest> validator, ISectorRepository<SectorEntity> repositorySector)
        {
            _validator = validator;
            _repositorySector = repositorySector;
        }

        public async Task<ApplicationResult<SectorResponse>> Handle(SectorCreateRequest request, CancellationToken cancellationToken)
        {
            var resultValidation = await _validator.ValidateAsync(request);

            if (!resultValidation.IsValid)
            {
                var res = resultValidation.Errors.Select(c => new ApplicationError(c.ErrorCode, c.ErrorMessage)).ToList();
                return new ApplicationResult<SectorResponse>().ReponseErrorFluentValidator(res);
            }

            var sectorEntity = request.Adapt<SectorEntity>();

            var result = await _repositorySector.InsertAsync(sectorEntity);

            return new ApplicationResult<SectorResponse>().ReponseSuccess(result.Adapt<SectorResponse>());
        }
    }
}

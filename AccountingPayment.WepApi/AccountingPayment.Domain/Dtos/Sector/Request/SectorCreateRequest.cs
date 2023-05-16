using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Dtos.Sector.Response;
using MediatR;

namespace AccountingPayment.Domain.Dtos.Sector.Request
{
    public class SectorCreateRequest : IRequest<ApplicationResult<SectorResponse>>
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
    }
}

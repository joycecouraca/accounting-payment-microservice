using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Dtos.Sector.Response;
using MediatR;

namespace AccountingPayment.Domain.Dtos.Sector.Request
{
    public class SectorCreateRequest : IRequest<ApplicationResult<SectorResponse>>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}

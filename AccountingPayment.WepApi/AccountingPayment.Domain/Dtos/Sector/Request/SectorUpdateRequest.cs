using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Dtos.Sector.Response;
using MediatR;

namespace AccountingPayment.Domain.Dtos.Sector.Request
{
    public class SectorUpdateRequest : SectorCreateRequest, IRequest<ApplicationResult<SectorResponse>>
    {
        public Guid Id { get; set; }
    }
}

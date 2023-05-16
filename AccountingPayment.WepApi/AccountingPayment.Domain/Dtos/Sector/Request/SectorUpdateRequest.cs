namespace AccountingPayment.Domain.Dtos.Sector.Request
{
    public class SectorUpdateRequest : SectorCreateRequest
    {
        public Guid Id { get; set; }
    }
}

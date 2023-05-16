namespace AccountingPayment.Domain.Dtos.Sector.Request
{
    public class SectorCreateRequest
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
    }
}

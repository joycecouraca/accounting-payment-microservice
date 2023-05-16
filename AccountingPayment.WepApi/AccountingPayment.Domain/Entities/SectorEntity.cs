namespace AccountingPayment.Domain.Entities
{
    public class SectorEntity : BaseEntity
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
    }
}

using AccountingPayment.Domain.Entities;

namespace AccountingPayment.Domain.Interfaces.Repository
{
    public interface ISectorRepository<T> : IRepository<T> where T : SectorEntity
    {
    }
}

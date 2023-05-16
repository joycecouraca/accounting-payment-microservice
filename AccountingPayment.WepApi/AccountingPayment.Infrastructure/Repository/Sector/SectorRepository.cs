using AccountingPayment.Domain.Entities;
using AccountingPayment.Domain.Interfaces.Repository;
using AccountingPayment.Infrastructure.Persistence.Context;

namespace AccountingPayment.Infrastructure.Repository.Sector
{
    public class SectorRepository : BaseRepository<SectorEntity>, ISectorRepository<SectorEntity>
    {
        public SectorRepository(SqlDbContext context) : base(context)
        {
        }
    }
}

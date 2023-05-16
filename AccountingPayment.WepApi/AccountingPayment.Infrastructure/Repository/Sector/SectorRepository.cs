using AccountingPayment.Domain.Entities;
using AccountingPayment.Domain.Interfaces.Repository;
using AccountingPayment.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AccountingPayment.Infrastructure.Repository.Sector
{
    public class SectorRepository : BaseRepository<SectorEntity>, ISectorRepository<SectorEntity>
    {
        public SectorRepository(SqlDbContext context) : base(context)
        {
        }
        public async override Task<IEnumerable<SectorEntity>> SelectAsync()
        {
            return await _dataset.Where(c => !c.Deleted).ToListAsync();
        }

        public async override Task<SectorEntity> SelectAsync(Guid id)
        {
            return await _dataset.SingleOrDefaultAsync(c => !c.Deleted);
        }
    }
}

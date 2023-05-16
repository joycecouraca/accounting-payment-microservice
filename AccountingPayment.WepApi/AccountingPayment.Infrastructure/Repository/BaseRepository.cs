using AccountingPayment.Domain.Entities;
using AccountingPayment.Domain.Interfaces.Repository;
using AccountingPayment.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AccountingPayment.Infrastructure.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SqlDbContext _context;
        public DbSet<T> _dataset;
        public BaseRepository(SqlDbContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _dataset.SingleOrDefaultAsync(t => t.Id == id);

            if (result == null)
                return false;

            result.SoftDeleteEntity();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            return await _dataset.AnyAsync(p => p.Id.Equals(id));
        }

        public async Task<T> InsertAsync(T item)
        {
            if (item.Id == Guid.Empty)
            {
                item.Id = Guid.NewGuid();
            }

            _dataset.Add(item);

            await _context.SaveChangesAsync();

            return item;
        }

        public virtual async Task<T> SelectAsync(Guid id)
        {
            return await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
        }

        public virtual async Task<IEnumerable<T>> SelectAsync()
        {
            return await _dataset.ToListAsync();
        }
        public async Task<T> UpdateAsync(T item)
        {
            var result = await _dataset.SingleOrDefaultAsync(t => t.Id == item.Id);
            if (result == null)
                return null;

            item.SetLastModified();

            _context.Entry(result).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();

            return item;
        }
    }
}

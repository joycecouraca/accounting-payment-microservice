using AccountingPayment.Domain.Entities;
using AccountingPayment.Domain.Interfaces.Repository;
using AccountingPayment.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AccountingPayment.Infrastructure.Repository.Employee
{
    public class EmployeeRepository : BaseRepository<EmployeeEntity>, IEmployeeRepository<EmployeeEntity>
    {
        public EmployeeRepository(SqlDbContext context) : base(context)
        {

        }

        public async override Task<IEnumerable<EmployeeEntity>> SelectAsync()
        {
            return await _dataset.Include(c => c.Sector).Where(c => !c.Deleted).ToListAsync();
        }
        public async override Task<EmployeeEntity> SelectAsync(Guid id)
        {
            return await _dataset.Include(c => c.Sector).SingleOrDefaultAsync(p => p.Id.Equals(id) && !p.Deleted);
        }

    }
}

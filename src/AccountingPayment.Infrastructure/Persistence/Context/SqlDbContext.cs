using AccountingPayment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AccountingPayment.Infrastructure.Persistence.Context
{
    public class SqlDbContext : DbContext
    {
        public DbSet<EmployeeEntity> Employee { get; set; }
        public DbSet<SectorEntity> Sector { get; set; }
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}

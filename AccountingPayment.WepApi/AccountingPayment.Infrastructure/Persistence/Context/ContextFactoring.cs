using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AccountingPayment.Infrastructure.Persistence.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<SqlDbContext>
    {
        public SqlDbContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=tcp:joyce-couraca-database.database.windows.net,1433;Initial Catalog=joyce-couraca.database;Persist Security Info=False;User ID=admin_database;Password=Db119244@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var optionsBuilder = new DbContextOptionsBuilder<SqlDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new SqlDbContext(optionsBuilder.Options);
        }
    }
}

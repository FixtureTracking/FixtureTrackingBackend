using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace FixtureTracking.DataAccess.Concrete.EntityFramework.Contexts
{
    public class FixtureTrackingContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql(_connectionString)
                .UseSnakeCaseNamingConvention();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Debit> Debits { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Fixture> Fixtures { get; set; }
        public DbSet<FixturePosition> FixturePositions { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }


        private static string _connectionString;
        public static void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}

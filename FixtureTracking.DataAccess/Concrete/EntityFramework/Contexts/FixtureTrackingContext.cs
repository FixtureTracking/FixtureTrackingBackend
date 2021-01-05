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
                .UseNpgsql(@"Server=localhost;Port=5432;Database=fixture_management;User Id=postgres;Password=1244")
                .UseSnakeCaseNamingConvention();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Debit> Debits { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Fixture> Fixtures { get; set; }
        public DbSet<FixturePosition> FixturePositions { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

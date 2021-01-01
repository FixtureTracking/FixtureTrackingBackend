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

        public DbSet<Fixture> Fixtures { get; set; }
    }
}

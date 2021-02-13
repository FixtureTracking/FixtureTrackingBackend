using FixtureTracking.Core.Entities;

namespace FixtureTracking.Entities.Dtos.Fixture
{
    public class FixtureForDetailDto : IDto
    {
        public Concrete.Fixture Fixture { get; set; }
        public string CategoryName { get; set; }
        public string SupplierName { get; set; }
        public string FixturePosName { get; set; }
    }
}

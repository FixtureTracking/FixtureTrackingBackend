using FixtureTracking.Core.Entities;

namespace FixtureTracking.Entities.Dtos.Supplier
{
    public class SupplierForAddDto : IDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

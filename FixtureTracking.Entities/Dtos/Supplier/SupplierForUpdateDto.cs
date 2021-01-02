using FixtureTracking.Core.Entities;

namespace FixtureTracking.Entities.Dtos.Supplier
{
    public class SupplierForUpdateDto : SupplierForAddDto, IDto
    {
        public int Id { get; set; }
    }
}

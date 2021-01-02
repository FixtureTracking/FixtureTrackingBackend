using FixtureTracking.Core.Entities;

namespace FixtureTracking.Entities.Dtos.Category
{
    public class CategoryForAddDto : IDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

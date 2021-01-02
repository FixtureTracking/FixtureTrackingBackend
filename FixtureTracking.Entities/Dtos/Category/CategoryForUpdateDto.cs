using FixtureTracking.Core.Entities;

namespace FixtureTracking.Entities.Dtos.Category
{
    public class CategoryForUpdateDto : CategoryForAddDto, IDto
    {
        public short Id { get; set; }
    }
}

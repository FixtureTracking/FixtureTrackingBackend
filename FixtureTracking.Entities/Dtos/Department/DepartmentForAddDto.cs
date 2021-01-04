using FixtureTracking.Core.Entities;

namespace FixtureTracking.Entities.Dtos.Department
{
    public class DepartmentForAddDto : IDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

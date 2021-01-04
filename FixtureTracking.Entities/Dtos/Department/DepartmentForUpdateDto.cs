using FixtureTracking.Core.Entities;

namespace FixtureTracking.Entities.Dtos.Department
{
    public class DepartmentForUpdateDto : DepartmentForAddDto, IDto
    {
        public int Id { get; set; }
    }
}

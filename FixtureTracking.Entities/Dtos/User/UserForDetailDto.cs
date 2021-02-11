using FixtureTracking.Core.Entities;

namespace FixtureTracking.Entities.Dtos.User
{
    public class UserForDetailDto : IDto
    {
        public Core.Entities.Concrete.User User { get; set; }
        public string FullName { get; set; }
        public string DepartmentName { get; set; }
    }
}

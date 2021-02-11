using FixtureTracking.Core.Entities;

namespace FixtureTracking.Entities.Dtos.Debit
{
    public class DebitForDetailDto : IDto
    {
        public Concrete.Debit Debit { get; set; }
        public string FixtureName { get; set; }
        public string FixtureDescription { get; set; }
        public string FixturePictureUrl { get; set; }
        public string UserFullName { get; set; }
        public string DepartmentName { get; set; }
    }
}

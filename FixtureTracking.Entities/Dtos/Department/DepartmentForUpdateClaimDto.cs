using FixtureTracking.Core.Entities;

namespace FixtureTracking.Entities.Dtos.Department
{
    public class DepartmentForUpdateClaimDto : IDto
    {
        public int Id { get; set; }
        public string[] OperationClaimNames { get; set; }
    }
}

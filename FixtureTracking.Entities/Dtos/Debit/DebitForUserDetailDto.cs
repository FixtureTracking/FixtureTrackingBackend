namespace FixtureTracking.Entities.Dtos.Debit
{
    public class DebitForUserDetailDto
    {
        public Concrete.Debit Debit { get; set; }
        public string UserFullName { get; set; }
        public string DepartmentName { get; set; }
    }
}

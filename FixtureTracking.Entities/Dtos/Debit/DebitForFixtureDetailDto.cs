namespace FixtureTracking.Entities.Dtos.Debit
{
    public class DebitForFixtureDetailDto
    {
        public Concrete.Debit Debit { get; set; }
        public string FixtureName { get; set; }
        public string FixtureDescription { get; set; }
        public string FixturePictureUrl { get; set; }
    }
}

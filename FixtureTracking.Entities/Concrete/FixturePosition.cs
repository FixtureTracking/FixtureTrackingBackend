using FixtureTracking.Core.Entities;

namespace FixtureTracking.Entities.Concrete
{
    public class FixturePosition : IEntity
    {
        public short Id { get; set; }
        public string Name { get; set; }
    }
}

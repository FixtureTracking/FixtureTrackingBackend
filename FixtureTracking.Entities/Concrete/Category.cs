using FixtureTracking.Core.Entities;
using System;

namespace FixtureTracking.Entities.Concrete
{
    public class Category : IEntity
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsEnable { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

using FixtureTracking.Core.Entities;
using System;

namespace FixtureTracking.Entities.Concrete
{
    public class Department : IEntity
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public bool IsEnable { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public short CompanyId { get; set; }
    }
}

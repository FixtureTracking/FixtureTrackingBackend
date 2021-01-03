using FixtureTracking.Core.Entities;
using System;

namespace FixtureTracking.Entities.Concrete
{
    public class Department : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsEnable { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string[] OperationClaimNames { get; set; }
    }
}

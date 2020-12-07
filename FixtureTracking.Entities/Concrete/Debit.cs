using FixtureTracking.Core.Entities;
using System;

namespace FixtureTracking.Entities.Concrete
{
    public class Debit : IEntity
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsReturn { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DateDebit { get; set; }
        public DateTime DateReturn { get; set; }
        public Guid FixtureId { get; set; }
        public Guid UserId { get; set; }
        public short DepartmentId { get; set; }
    }
}

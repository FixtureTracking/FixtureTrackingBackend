using FixtureTracking.Core.Entities;
using System;

namespace FixtureTracking.Entities.Dtos.Debit
{
    public class DebitForAddDto : IDto
    {
        public string Description { get; set; }
        public DateTime DateDebit { get; set; }
        public Guid FixtureId { get; set; }
        public Guid UserId { get; set; }
    }
}

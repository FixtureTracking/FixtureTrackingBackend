using FixtureTracking.Core.Entities;
using System;

namespace FixtureTracking.Entities.Dtos.Debit
{
    public class DebitForUpdateDto : IDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime DateDebit { get; set; }
    }
}

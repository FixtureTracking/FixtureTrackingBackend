using FixtureTracking.Core.Entities;
using System;

namespace FixtureTracking.Entities.Dtos.Company
{
    public class CompanyForUpdateDto : IDto
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

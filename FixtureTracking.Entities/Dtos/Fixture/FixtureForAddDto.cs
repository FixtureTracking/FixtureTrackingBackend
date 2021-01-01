using FixtureTracking.Core.Entities;
using System;

namespace FixtureTracking.Entities.Dtos.Fixture
{
    public class FixtureForAddDto : IDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public DateTime DatePurchase { get; set; }
        public DateTime DateWarranty { get; set; }
        public int SupplierId { get; set; }
        public short CategoryId { get; set; }
    }
}

using FixtureTracking.Core.Entities;
using System;

namespace FixtureTracking.Entities.Concrete
{
    public class Fixture : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DatePurchase { get; set; }
        public DateTime DateWarranty { get; set; }
        public int SupplierId { get; set; }
        public short CategoryId { get; set; }
        public short FixturePositionId { get; set; }
    }
}

using FixtureTracking.Core.Entities;
using System;

namespace FixtureTracking.Entities.Dtos.Fixture
{
    public class FixtureForUpdateDto : FixtureForAddDto, IDto
    {
        public Guid Id { get; set; }
    }
}

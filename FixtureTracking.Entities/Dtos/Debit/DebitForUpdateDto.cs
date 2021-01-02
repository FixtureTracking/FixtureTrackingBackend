using FixtureTracking.Core.Entities;
using System;

namespace FixtureTracking.Entities.Dtos.Debit
{
    public class DebitForUpdateDto : DebitForAddDto, IDto
    {
        public Guid Id { get; set; }
    }
}

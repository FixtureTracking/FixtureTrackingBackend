using FixtureTracking.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FixtureTracking.Entities.Dtos.Department
{
    public class DepartmentForAddDto : IDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

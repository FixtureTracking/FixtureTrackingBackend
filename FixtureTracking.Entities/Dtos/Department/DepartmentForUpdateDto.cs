using FixtureTracking.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FixtureTracking.Entities.Dtos.Department
{
    public class DepartmentForUpdateDto : DepartmentForAddDto, IDto
    {
        public int Id { get; set; }
    }
}

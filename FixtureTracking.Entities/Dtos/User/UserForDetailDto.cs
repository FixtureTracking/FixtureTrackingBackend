using FixtureTracking.Core.Entities;
using System;

namespace FixtureTracking.Entities.Dtos.User
{
    public class UserForDetailDto : IDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}

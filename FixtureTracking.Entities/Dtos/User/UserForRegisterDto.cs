﻿using FixtureTracking.Core.Entities;
using System;

namespace FixtureTracking.Entities.Dtos.User
{
    public class UserForRegisterDto : IDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public int DepartmentId { get; set; }
    }
}

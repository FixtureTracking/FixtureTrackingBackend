using FixtureTracking.Core.Entities;

namespace FixtureTracking.Entities.Dtos.User
{
    public class UserForLoginDto : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

namespace FixtureTracking.Core.Utilities.CustomExceptions
{
    public class AuthorizationException : HttpStatusException
    {
        public AuthorizationException(string message) : base(message, 401) { }
    }
}

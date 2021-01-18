using System;

namespace FixtureTracking.Core.Utilities.CustomExceptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string message) : base(message) { }
    }
}

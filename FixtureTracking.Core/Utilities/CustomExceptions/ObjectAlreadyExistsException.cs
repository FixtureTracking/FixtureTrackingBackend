using System;

namespace FixtureTracking.Core.Utilities.CustomExceptions
{
    public class ObjectAlreadyExistsException : Exception
    {
        public ObjectAlreadyExistsException(string message) : base(message) { }
    }
}

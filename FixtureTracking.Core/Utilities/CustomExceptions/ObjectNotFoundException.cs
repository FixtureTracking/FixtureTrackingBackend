using System;

namespace FixtureTracking.Core.Utilities.CustomExceptions
{
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException(string message) : base(message) { }
    }
}

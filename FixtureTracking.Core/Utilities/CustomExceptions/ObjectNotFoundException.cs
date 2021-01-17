using FixtureTracking.Core.Utilities.Messages;
using System;

namespace FixtureTracking.Core.Utilities.CustomExceptions
{
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException(string objectName) : base(ExceptionMessages.ObjectNotFound(objectName)) { }
    }
}

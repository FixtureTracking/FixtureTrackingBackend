namespace FixtureTracking.Core.Utilities.CustomExceptions
{
    public class ObjectAlreadyExistsException : HttpStatusException
    {
        public ObjectAlreadyExistsException(string message) : base(message, 400) { }
    }
}

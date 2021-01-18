namespace FixtureTracking.Core.Utilities.CustomExceptions
{
    public class ObjectNotFoundException : HttpStatusException
    {
        public ObjectNotFoundException(string message) : base(message, 404) { }
    }
}

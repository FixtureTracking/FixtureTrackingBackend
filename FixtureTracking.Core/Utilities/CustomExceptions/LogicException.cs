namespace FixtureTracking.Core.Utilities.CustomExceptions
{
    public class LogicException : HttpStatusException
    {
        public LogicException(string message) : base(message, 400) { }
    }
}

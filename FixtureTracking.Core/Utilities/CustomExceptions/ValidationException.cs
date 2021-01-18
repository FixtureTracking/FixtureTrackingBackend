namespace FixtureTracking.Core.Utilities.CustomExceptions
{
    public class ValidationException : HttpStatusException
    {
        public ValidationException(string message) : base(message, 400)
        {
        }
    }
}

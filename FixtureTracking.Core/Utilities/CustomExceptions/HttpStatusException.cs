using System;

namespace FixtureTracking.Core.Utilities.CustomExceptions
{
    public abstract class HttpStatusException : Exception
    {
        public int HttpStatusCode { get; private set; }
        public HttpStatusException(string message, int httpStatusCode) : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}
